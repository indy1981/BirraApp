import axios from 'axios';
import jwt_decode from 'jwt-decode';

const BIRRAS_BASE_API_URL = 'https://localhost:44327/api';
class AuthService {
    async login(username, password){        
        try {
            const response = await axios.post(`${BIRRAS_BASE_API_URL}/Users/login`, {username, password})
            localStorage.setItem('token', response.data.accessToken);
            localStorage.setItem('userId', response.data.id);
            localStorage.setItem('userName', response.data.username);
            return { user: response.data }
        } catch (err) {            
            if (!err.response || !err.response.data){
            
                return {errorMessage: "Error al intentar hacer login, intentelo de nuevo mas tarde"};
            }
            
            return { errorMessage: err.response.data };
        }        
    }

    GetUsername(){
        return localStorage.userName;
    }

    GetUserId(){
        return localStorage.userId;
    }

    async logOut(){
        localStorage.removeItem('token');        
    }

    GetLoggedAdminToken(){
        const tokenStored = localStorage.token;
        if (!tokenStored)
            return null;

        const decodeUser = jwt_decode(tokenStored);

        if (!decodeUser ||
            !decodeUser.role ||
            decodeUser.role !== "Admin")
        {
            return null
        }
        return tokenStored;
    }

    GetLoggedUserToken(){
        const tokenStored = localStorage.token;        
        if (!tokenStored)
            return null;

        const decodeUser = jwt_decode(tokenStored);        
        if (!decodeUser ||
            !decodeUser.role ||
            decodeUser.role !== "User")
        {
            return null
        }
        return tokenStored;
    }

    GetLogged
}

export default new AuthService();