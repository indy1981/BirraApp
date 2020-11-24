import axios from 'axios';

const BIRRAS_BASE_API_URL = 'https://localhost:44327/api';

class MeetupsService{
    async GetMyInvitedMeetUps(){
        try{
            const token = localStorage.token;
            const userId = localStorage.userId;
            const response = await axios.get(`${BIRRAS_BASE_API_URL}/meetups/user/${userId}/me/invites`, { headers: { 'Authorization' : 'bearer ' + token }});                
            return response.data;
        }
        catch(ex){
            return null;
        }
    }

    async GetAllMeetUps (){
        try{
            const token = localStorage.token;
            const response = await axios.get(`${BIRRAS_BASE_API_URL}/meetups/all`, { headers: { 'Authorization' : 'bearer ' + token }});
            return response.data;
        }
        catch(ex){
            return null;
        }
    }

    async GetAllRequests  (){
        try{
            const token = localStorage.token;
            const response = await axios.get(`${BIRRAS_BASE_API_URL}/meetups/all/requests`, { headers: { 'Authorization' : 'bearer ' + token }});
            return response.data;
        }
        catch(ex){
            return null;
        }
    }

    async GetAllMeetupsUserWasNotInvited(userId){
        try{
            const token = localStorage.token;                        
            const response = await axios.get(`${BIRRAS_BASE_API_URL}/meetups/user/${userId}/me/not/invites`, { headers: { 'Authorization' : 'bearer ' + token }});
            return response.data;
        }
        catch(ex){
            return null;
        }
    }

    async GetWeather(date){
        const token = localStorage.token;
        try{
            const response = await axios.get(`${BIRRAS_BASE_API_URL}/meetups/weather/${new Date(date).toISOString()}`, { headers: { 'Authorization' : 'bearer ' + token }});
            return response.data;
        }
        catch(ex){
            return null;
        }
    }

    async CreateRequest(userId, meetupId){
        try{
            const token = localStorage.token;        
            const response = await axios.post(`${BIRRAS_BASE_API_URL}/meetups/user/request`, { 
                userId, 
                meetupId
            },
            { headers: { 'Authorization' : 'bearer ' + token }});
            
            return (response.status === 200);
        }
        catch(ex){
            return false;
        }
    }

    async CreateInvite(userId, meetupId){
        try{
            const token = localStorage.token;        
            const response = await axios.post(`${BIRRAS_BASE_API_URL}/meetups/create/invite`, { 
                userId, 
                meetupId
            },
            { headers: { 'Authorization' : 'bearer ' + token }});
            
            return (response.status === 200);
        }
        catch(ex){
            return false;
        }
    }

    async MarkAssistance(userId, meetupId){
        try{
            const token = localStorage.token;             
            const response = await axios.put(`${BIRRAS_BASE_API_URL}/meetups/user/${userId}/me/checkAsistance/${meetupId}`, { 
                userId, 
                meetupId
            },
            { headers: { 'Authorization' : 'bearer ' + token }});
            
            return (response.status === 204);
        }
        catch(ex){
            return false;
        }
    }

    async CreateMeetup(name, meetUpDate, inviteesIds) {
        try{
            const token = localStorage.token;        
            const response = await axios.post(`${BIRRAS_BASE_API_URL}/meetups/create`, { 
                name, 
                meetUpDate, 
                inviteesIds } ,
                { headers: { 'Authorization' : 'bearer ' + token }});
            
            return response.data;
        }
        catch(ex){
            return false;
        }
    }
}

export default new MeetupsService();