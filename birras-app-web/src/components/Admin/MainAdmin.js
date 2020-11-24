import React, { useState } from 'react';
import AuthService from '../../services/auth.service';
import MeetupsService from '../../services/meetups.service';
import Table from '../Shared/Table';
import { withRouter } from 'react-router-dom';
import { NavLink } from "react-router-dom";

const MainAdmin = (props) => {

    const [meetUps, setMeetups] = useState([]);

    const FetchAllMeetUps = React.useCallback( async () =>{
        const data = await MeetupsService.GetAllMeetUps();
        setMeetups(data);
    }, []);

      React.useEffect( () => {
        var loggedAdminToken = AuthService.GetLoggedAdminToken();        
        if (!loggedAdminToken)
        {
            props.history.push('/login');
            return;
        }
        FetchAllMeetUps();
    },[FetchAllMeetUps , props.history]);

    return (
        <div>
            <h3>Main Admin Page</h3>
            <div>
                <Table columns= {columns} data={meetUps} />
            </div>
            <div>
            
            <NavLink className="btn btn-primary" to="/addMeetup" role="button">Crear Meetup</NavLink>
            </div>
        </div>
    )
}

export default withRouter(MainAdmin);

export const columns = [
    {
      Header: 'Id',
      accessor: 'id',
    },
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
    }
    ,
    {
      Header: 'Packs de birra',
      accessor: 'birrasPack',
    }
  ];
  