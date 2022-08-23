import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import FloatingLabel from 'react-bootstrap/esm/FloatingLabel';
import axios from 'axios';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';

function AddCharacter(props) {
    let domain = process.env.REACT_APP_BACKEND
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const [character, setCharacter] = useState({ id: 0, icName: '', userId: 0, skills: [] });
    const [validated, setValidated] = useState(false);
    const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();

    const navigate = useNavigate();

    useEffect(() => {
        setCharacter({...character, userId: props.userId})
    }, [props])

    const handleSubmit = async (event) => {
        const form = event.currentTarget;
        event.preventDefault();

        if (form.checkValidity() === false) {
            event.stopPropagation();
        }

        setValidated(true)

        try {
            const accessToken = await getAccessTokenSilently({ audience: `https://characters-aren.azurewebsites.net`, scope: "" });
            let header = { headers: { Authorization: `Bearer ${accessToken}` } }
            const response = await axios.post(domain + '/api/Characters', character, header);
            handleClose();
            navigate("./" + response.data.id)
        }
        catch {
            window.alert("item niet aangemaakt!");
        }
    }

    const updateField = e => {
        setCharacter({
            ...character,
            [e.target.name]: e.target.value
        });
    };
    return (
        <>
            <Button variant="primary" onClick={handleShow}>
                Karakter Toevoegen
            </Button>

            <Modal show={show} onHide={handleClose} size="lg">
                <Modal.Header closeButton>
                    <Modal.Title>Nieuw Karakter</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form id="add-skill-form" noValidate validated={validated} onSubmit={handleSubmit}>
                        <Form.Group as={Row} className="mb-3" controlId="formName">
                            <Form.Label column sm="2">
                                Naam:
                            </Form.Label>
                            <Col sm="10">
                                <Form.Control type="text" placeholder="karakter's naam" name="icName" onChange={updateField} required />
                                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
                            </Col>
                        </Form.Group>               
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button form="add-skill-form" variant="primary" type="submit">
                        Opslaan
                    </Button>
                    <Button variant="secondary" onClick={handleClose}>
                        Sluiten
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
}

export default AddCharacter