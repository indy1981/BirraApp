import React from 'react';
import AuthService from '../services/auth.service';
import { withRouter } from 'react-router-dom';

const Login = (props) => {

    const [username, setUsername] = React.useState('');
    const [password, setPassword] = React.useState('');
    const [error, setError] = React.useState(null);

    const ProcessLogin = async e =>{        
        e.preventDefault();
        if (!username.trim()){
            setError('Email invalido');
            return;
        }

        if (!password.trim()){
            setError('Password invalido');
            return;
        }

        if (password.length < 8 || password.length > 30){            
            setError('El Password debe tener entre 8 y 30 caracteres');
            return;
        }
        setError(null);
        registrar();
    }

    const registrar = React.useCallback( async () =>{
        try {
            const loginResponse = await AuthService.login(username, password);            
            if (loginResponse.errorMessage){
                setError(loginResponse.errorMessage);
                return;
            }
            setPassword('');
            setUsername('');
            setError(null);
            
            if (AuthService.GetLoggedAdminToken()){
                props.history.push('/admin');
                props.handleLogin();
                return;
            }

            props.history.push('/user');
            props.handleLogin();

        } catch (error) {
            setError("Ocurrio un error al login, intente de nuevo mas tardes");
            return;
        }  
    },[username, password, props]);

    return (
        <div className="mt-5">
            <h3 className="text-center">Birra App login</h3>
            <hr/>            
            <div className="row justify-content-center">
                <div className="row mr-4">
                    <img height="150px" width="150px" src="/images/birraapp-logo.png"></img>
                </div>
                <div className="col-12 col-sm-8 col-md-6 col-xl-4">
                    <form onSubmit = {ProcessLogin}>
                        <input 
                            type="text" 
                            className="form-control mb-2" 
                            placeholder="username" 
                            onChange={ (e)=>{ setUsername(e.target.value) } } 
                            value={username}>                                
                        </input>
                        <input 
                            type="password" 
                            className="form-control mb-2" 
                            placeholder="password" 
                            onChange={ (e)=>{ setPassword (e.target.value)}} 
                            value={password}>
                        </input>
                        <button className="btn btn-info btn-lg btn-block" >Registrarse</button>                        
                        {
                            error && (<div className="alert alert-danger">{error}</div>)
                        }
                    </form>
                </div>

            </div>

        </div>
    )
}

export default withRouter(Login)