import React, {useState} from 'react';
import Table from '../Shared/Table';
import UsersService from '../../services/users.service';
import AuthService from '../../services/auth.service';
import MeetupsService from '../../services/meetups.service';
import Calendar from 'react-calendar'
import 'react-calendar/dist/Calendar.css';
import { withRouter } from 'react-router-dom';
import Modal from '../Shared/Modal';

const AddMeetUp = (props) => {
    
    const [meetupDate, setMeetupDate] = useState(new Date());
    const [error, setError] = useState(null);
    const [dayTemperature, setDayTemperature] = useState('');
    const [users, setUsers] = useState([]);
    const [meetupName, setMeetupName] = useState('');
    const [messageModal, setMessageModal] = useState('');
    const [showModal, setShowModal] = useState(false);

    const handleCloseModal = () => {setShowModal(false);};
    const handleShowModal = () => setShowModal(true);

    const handleAddMetupSubmit = async event =>{
        event.preventDefault();

        const inviteesIds = users.filter( user => user.invite ).map( x=> x.id );

        if (meetupName === ''){
            setError('Una meetup debe un nombre');
            return;
        }

        if (inviteesIds.length === 0){
            setError('Una meetup debe tener al menos un invitado');
            return;
        }

        if (meetupDate > new Date().setDate(new Date().getDate()+6)){
            setError('No hay pronostico para mas de una semana');
            return;
        }

        if (meetupDate < new Date().setHours(0,0,0,0)){
            setError('No se puede hacer un meetup en el pasado');
            return;
        }

        setError(null);
        const creationResponse = await MeetupsService.CreateMeetup( meetupName, meetupDate, inviteesIds);
        if (creationResponse){
            console.log(creationResponse);
            setMessageModal(`Meetup creada con exito! necesitaras: ${creationResponse.birrasPack} pack(s) de birra`);
            handleShowModal();
            return;
        }
        setMessageModal("Hubo un problema al crear la meetup");
        handleShowModal();
    }

    const addUserToInvite = cell => {
        var id = parseInt(cell.target.getAttribute('user'));
        var user = users.find( user => user.id === id);
        user.invite = true;
        setUsers([...users]);        
    }

    const removeUserFromInvite = cell => {
        var id = parseInt(cell.target.getAttribute('user'));
        var user = users.find( user => user.id === id);
        user.invite = false;
        setUsers([...users]);        
    }

    const FetchAllUsers = React.useCallback( async () =>{
        const usersFromDatabase = await UsersService.GetAllUsers();
        const usersForTable = usersFromDatabase.map( user=> ({...user, invite: false }) )
        setUsers(usersForTable);        
    }, []);

    const handleDate = async (date) =>{
        const temperature = await MeetupsService.GetWeather(date);
        
        if (!temperature){
            setDayTemperature('No hay datos para esa fecha');
            return;
        }
        setDayTemperature(temperature);
        setMeetupDate(date);
    }

    React.useEffect( () => {
        var loggedAdminToken = AuthService.GetLoggedAdminToken();        
        if (!loggedAdminToken)
        {
            props.history.push('/login');
            return;
        }

        FetchAllUsers();
        handleDate(new Date());
    },[FetchAllUsers , props.history]);


    const usersTableColumns = [
        {
            Header: 'Id',
            accessor: 'id',
        },
        {
            Header: 'Nombre de usuario',
            accessor: 'username',
        },
        {
            Header: 'Email',
            accessor: 'email',
        },
        {
            Header: 'Invitar',
            accessor : 'invite',
            Cell: ({ cell }) => {
                if (!cell.row.values.invite){
                    return (<button value='Invitar' user={cell.row.values.id} className="btn btn-primary" onClick= { addUserToInvite.bind(this)}>
                        Invitar
                    </button>)
                }
                else{
                    return (<button value='Invitar' user={cell.row.values.id} className="btn btn-info" onClick= {removeUserFromInvite.bind(this)}>
                        Eliminar
                    </button>)
                }
            }
        }
    ];

    return (         
    <div className="container">
            <h3 className= "mb-5">Crear una nueva Meetup</h3>
            <form onSubmit={handleAddMetupSubmit}>
                <div className="form-group">
                    <label htmlFor="name">Nombre de la Meetup</label>
                    <input type="text" className="form-control" onChange={ (e)=>{ setMeetupName(e.target.value)}}  value={ meetupName } placeholder="Titulo o motivo de la meetup" />                    
                </div>               
                <div className ="row">               
                    <div className="col-sm">
                    <Calendar 
                        onChange={handleDate}
                        value={meetupDate}
                        />
                    </div>
                    <div className="col-sm">
                        <h6>Temperatura del dia: {dayTemperature}</h6>
                    </div>
                    </div>
                <div>
                    <Table columns= {usersTableColumns} data= {users} />
                </div>
                <button type="submit" className="btn btn-primary">Crear meeting</button>
            </form>
            {
                error && (<div className="alert alert-danger">{error}</div>)
            }
        <Modal  handleOpenModal= {showModal} handleCloseModal={handleCloseModal} text={messageModal} title="BirraApp"/>
    </div>
    
    );
}

export default withRouter(AddMeetUp);

