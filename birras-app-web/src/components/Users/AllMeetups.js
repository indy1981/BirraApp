import React, { useState } from 'react';
import AuthService from '../../services/auth.service';
import MeetupsService from '../../services/meetups.service';
import Table from '../Shared/Table';
import { withRouter } from 'react-router-dom';
import Modal from '../Shared/Modal';
import authService from '../../services/auth.service';

const AllMeetups = (props) => {

    const [meetups, setMeetups] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [modalText, setModalText] = useState('');

    const handleCloseModal = () => {setShowModal(false);};
    const handleShowModal = () => setShowModal(true);

    const requestInvite  = async (cell) =>{
        var userId = authService.GetUserId();
        var meetupId = parseInt(cell.target.getAttribute('meetupid'));
        var responseCreateInvite = await MeetupsService.CreateRequest(userId, meetupId);
        if (!responseCreateInvite){
            setModalText('Hubo un problema al solicitar la invitacion');
        }
        else{
            setModalText('Invitacion solicitada con exito');
        }
        handleShowModal();
        FetchGetAllMeetupsUserWasNotInvited();
    };

    const FetchGetAllMeetupsUserWasNotInvited = React.useCallback( async () =>{
        var userId = authService.GetUserId();
        const data = await MeetupsService.GetAllMeetupsUserWasNotInvited(userId);
        setMeetups(data);
    }, []);

    React.useEffect( () => {
        var loggedUserToken = AuthService.GetLoggedUserToken();        
        if (!loggedUserToken)
        {
            props.history.push('/login');
            return;
        }
        FetchGetAllMeetupsUserWasNotInvited();
    },[FetchGetAllMeetupsUserWasNotInvited , props.history]);

    const columns = [
        {
            show: false,
            accessor: 'id',
        },
        {
            Header: 'Nombre de la Meetup',
            accessor: 'name',
        },        
        {
            Header: 'Fecha de la meet up',
            accessor: 'meetupDate',
        },
        {
            Header: 'Temperatura',
            accessor: 'temperature',
        },
        {
            Header: 'Solicitar',
            accessor : 'invite',
            Cell: ({ cell }) => {                 
                    return (<button value='Solicitar invitacion' 
                        meetupid={cell.row.values['id']} 
                        className="btn btn-primary" 
                        onClick= { requestInvite.bind(this)}>Solicitar</button>);
                }
        }
    ];

    return (
        <div>
            <h3>Meetups sin invitacion</h3>
            <div>
                <Table columns= {columns} data={meetups} />
            </div>
            <div>
            </div>
            <Modal  handleOpenModal= {showModal} handleCloseModal={handleCloseModal} text={modalText} title="BirraApp"/>
        </div>
    )
}

export default withRouter(AllMeetups);