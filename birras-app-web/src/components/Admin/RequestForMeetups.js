import React, { useState } from 'react';
import AuthService from '../../services/auth.service';
import MeetupsService from '../../services/meetups.service';
import Table from '../Shared/Table';
import { withRouter } from 'react-router-dom';
import Modal from '../Shared/Modal';

const RequestForMeetups = (props) => {

    const [requests, setRequests] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [modalText, setModalText] = useState('');

    const handleCloseModal = () => {setShowModal(false);};
    const handleShowModal = () => setShowModal(true);

    const acceptInvite  = async (cell) =>{
        var userId = parseInt(cell.target.getAttribute('userid'));
        var meetupId = parseInt(cell.target.getAttribute('meetupid'));
        var responseCreateInvite = await MeetupsService.CreateInvite(userId, meetupId);
        if (!responseCreateInvite){
            setModalText('Hubo un problema al aceptar la invitacion');
        }
        else{
            setModalText('Invitacion enviada con exito');
        }
        handleShowModal();
        FetchAllRequests();
    };

    const FetchAllRequests = React.useCallback( async () =>{
        const data = await MeetupsService.GetAllRequests();
        setRequests(data);
    }, []);

    React.useEffect( () => {
        var loggedAdminToken = AuthService.GetLoggedAdminToken();
        if (!loggedAdminToken)
        {
            props.history.push('/login');
            return;
        }
        FetchAllRequests();
    },[FetchAllRequests , props.history]);

    const columns = [        
        {
        Header: 'Usuario',
        accessor: 'user.username',
        },
        {
        Header: 'Nombre de la Meetup',
        accessor: 'meetup.name',
        },        
        {
        Header: 'Fecha de la meet up',
        accessor: 'meetup.meetupDate',
        },
        {
            show: false,
            accessor: 'user.id',
        },
        {
            show: false,
            accessor: 'meetup.id',
        },
        {
            Header: 'Invitar',
            accessor : 'invite',
            Cell: ({ cell }) => {                 
                    return (<button value='Invitar' 
                        userid ={cell.row.values['user.id']} 
                        meetupid={cell.row.values['meetup.id']} 
                        className="btn btn-primary" 
                        onClick= { acceptInvite.bind(this)}>Aceptar</button>);
                }
        }
    ];

    return (
        <div>
            <h3>Invitaciones requeridas por usuarios</h3>
            <div>
                <Table columns= {columns} data={requests} />
            </div>
            <div>
            </div>
            <Modal  handleOpenModal= {showModal} handleCloseModal={handleCloseModal} text={modalText} title="BirraApp"/>
        </div>
    )
}

export default withRouter(RequestForMeetups);