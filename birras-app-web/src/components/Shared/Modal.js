import React from 'react';
import ReactModal from 'react-modal';
const customStyles = {
    content : {
        top                   : '50%',
        left                  : '50%',
        right                 : 'auto',
        bottom                : 'auto',
        marginRight           : '-50%',
        transform             : 'translate(-50%, -50%)'
    }
    };
    
export const Modal = (props) => {    
        return (
        <div>
            <ReactModal 
            isOpen={props.handleOpenModal}
            contentLabel="Minimal Modal Example"
            style={customStyles}
            >
                <div>
                <h3>{props.title}</h3>
                    {props.text}
                </div>
            <button className="btn btn-primary" onClick={props.handleCloseModal}>Cerrar</button>
            </ReactModal>
        </div>
        );    
}

export default Modal;