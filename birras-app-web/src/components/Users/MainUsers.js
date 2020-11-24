import React, { useState } from 'react';
import { withRouter } from 'react-router-dom';
import AuthService from '../../services/auth.service';
import MeetupsService from '../../services/meetups.service';
import Table from '../Shared/Table';
import Modal from '../Shared/Modal';
import authService from '../../services/auth.service';

const MainUser = (props) => {

    const [meetUps, setMeetups] = useState([]);
    const [showModal, setShowModal] = useState(false);

    const handleCloseModal = () => {setShowModal(false);};
    const handleShowModal = () => setShowModal(true);
    const [modalText, setModalText] = useState('');

    const FetchMyInvitesMeetups = React.useCallback( async () =>{
        const data = await MeetupsService.GetMyInvitedMeetUps();
        setMeetups(data);
    }, []);

    const markAssistance  = async (cell) =>{
        var userId = authService.GetUserId();
        var meetupId = parseInt(cell.target.getAttribute('meetupid'));
        var responseMarkAssistance = await MeetupsService.MarkAssistance(userId, meetupId);
        if (!responseMarkAssistance){
            setModalText('Hubo un problema al marcar la asistencia');
        }
        else{
            setModalText('Asistencia marcada con exito');
        }
        handleShowModal();
        FetchMyInvitesMeetups();
    };

    React.useEffect( () => {
        var loggedUserToken = AuthService.GetLoggedUserToken();        
        
        if (!loggedUserToken)
        {
            props.history.push('/login');
            return;
        }
        FetchMyInvitesMeetups();
    },[FetchMyInvitesMeetups , props.history]);

    const columns = [    
        {
            Header: 'Nombre',
            accessor: 'name',
        },
        {
            Header: 'Fecha',
            accessor: 'meetupDate',
        },
        {
            Header: 'Temperatura',
            accessor: 'temperature',  
        },
        {        
            show: false,
            accessor: 'id',
        },    
        {
        Header: 'Marcar asistencia',
        accessor : 'checkedIn',
        Cell: ({ cell }) => {
            if (!cell.row.values.checkedIn){
                return (<button 
                    value='Asistencia'                     
                    meetupid={cell.row.values['id']} 
                    className="btn btn-primary"
                    onClick= { markAssistance.bind(this)}>
                    Marcar asistencia
                </button>)
            }
                return (<h6>Asistido</h6>)
            }
        }
    ];

    return (
        <div>
            <h3 className= "mb-5">Bienvenido usuario {AuthService.GetUsername()}</h3>            
            <div>
                <Table columns= {columns} data={meetUps} />
            </div>
            <Modal  handleOpenModal= {showModal} handleCloseModal={handleCloseModal} title={"BirraApp"} text={modalText} />
        </div>
    )
}

export default withRouter(MainUser);