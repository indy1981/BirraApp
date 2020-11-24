import axios from 'axios';

const BIRRAS_BASE_API_URL = 'https://localhost:44327/api';
class UsersService {

    async GetAllUsers(){
        const token = localStorage.token;
        const response = await axios.get(`${BIRRAS_BASE_API_URL}/users/allUsers`, { headers: { 'Authorization' : 'bearer ' + token }});
        return response.data;
    }
}

export default new UsersService();