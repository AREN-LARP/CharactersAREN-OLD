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

function AddEvent(props) {
    let domain = process.env.REACT_APP_BACKEND
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const [event, setEvent] = useState({ id: 0, name: '', data: 0});
    const [validated, setValidated] = useState(false);
    const [users, setUsers] = useState([]);
    const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();

    const navigate = useNavigate();
    useEffect(async () => {
        const accessToken = await getAccessTokenSilently({ audience: `https://characters-aren.azurewebsites.net`, scope: "" });
        let header = { headers: { Authorization: `Bearer ${accessToken}` } }
    }, [])

    const handleSubmit = async (event) => {
        console.log(event)
        const form = event.currentTarget;
        event.preventDefault();

        if (form.checkValidity() === false) {
            event.stopPropagation();
        }

        setValidated(true)

        try {
            const accessToken = await getAccessTokenSilently({ audience: `https://characters-aren.azurewebsites.net`, scope: "" });
            let header = { headers: { Authorization: `Bearer ${accessToken}` } }
            const response = await axios.post(domain + '/api/events', event, header);
            handleClose();
            navigate("./" + response.data.id)
        }
        catch {
            window.alert("item niet aangemaakt!");
        }
    }

    const updateField = e => {
        setEvent({
            ...event,
            [e.target.name]: e.target.value
        });
    };
    return (
        <>
            <Button variant="primary" onClick={handleShow}>
                Evenement Toevoegen
            </Button>

            <Modal show={show} onHide={handleClose} size="lg">
                <Modal.Header closeButton>
                    <Modal.Title>Nieuw Evenement</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form id="add-skill-form" noValidate validated={validated} onSubmit={handleSubmit}>
                        <Form.Group as={Row} className="mb-3" controlId="formName">
                            <Form.Label column sm="2">
                                Naam:
                            </Form.Label>
                            <Col sm="10">
                                <Form.Control type="text" placeholder="evenement's naam" name="name" onChange={updateField} required />
                                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
                            </Col>
                        </Form.Group>
                        <Form.Group>
                            <Row className="mb-3">
                                <Col sm="7">
                                    <FloatingLabel controlId="dateSelect" label="Datum">
                                        <Form.Select aria-label="Date" name="data" onChange={updateField} required>
                                            
                                        </Form.Select>
                                    </FloatingLabel>
                                </Col>                               
                            </Row>
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