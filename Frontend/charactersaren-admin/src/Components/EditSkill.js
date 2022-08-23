import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import FloatingLabel from 'react-bootstrap/esm/FloatingLabel';
import axios from 'axios';
import { useState } from 'react';
import { useAuth0 } from '@auth0/auth0-react';

function EditSkill(props) {
    let domain = process.env.REACT_APP_BACKEND
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const [skill, setSkill] = useState({ id: props.id, name: props.name, level: props.level, skillCategoryId: props.skillCategoryId, description: props.description });
    const [validated, setValidated] = useState(false);
    const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();

    const handleSubmit = async (event) => {
        event.preventDefault();
        const form = event.currentTarget;

        if (form.checkValidity() === false) {

            event.stopPropagation();
        }

        setValidated(true)
        try {
            const accessToken = await getAccessTokenSilently({ audience: `https://characters-aren.azurewebsites.net`, scope: "" });
            let header = { headers: { Authorization: `Bearer ${accessToken}` } }
            const response = await axios.put(domain + '/api/Skills/' + skill.id, skill, header);
            setSkill(response.data)
            handleClose();
            props.reload();
        }
        catch {
            window.alert("item niet aangepast!");
        }
    }

    const updateField = e => {
        setSkill({
            ...skill,
            [e.target.name]: e.target.value
        });
    };
    return (
        <>
            <Button variant="primary" onClick={handleShow}>
                Aanpassen
            </Button>

            <Modal show={show} onHide={handleClose} size="lg">
                <Modal.Header closeButton>
                    <Modal.Title>Update Skill</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form id="add-skill-form" noValidate validated={validated} onSubmit={handleSubmit}>
                        <Form.Group as={Row} className="mb-3" controlId="formSkill">
                            <Form.Label column sm="2">
                                Naam:
                            </Form.Label>
                            <Col sm="10">
                                <Form.Control type="text" name="name" onChange={updateField} value={skill.name} required />
                                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
                            </Col>
                        </Form.Group>
                        <Form.Group>
                            <Row className="mb-3">
                                <Col sm="7">
                                    <FloatingLabel controlId="CategorySelect" label="Skill Categorie">
                                        <Form.Select aria-label="Category" name="skillCategoryId" value={skill.skillCategoryId} onChange={updateField} required>
                                            {
                                                props.categories.map(category => (
                                                    <option key={category.id} value={category.id}>{category.name}</option>
                                                ))
                                            }
                                        </Form.Select>
                                    </FloatingLabel>
                                </Col>
                                <Col>
                                    <FloatingLabel controlId="LevelSelect" label="Skill Level">
                                        <Form.Select aria-label="Level" name="level" value={skill.level} onChange={updateField} required>
                                            <option value="1">1</option>
                                            <option value="2">2</option>
                                            <option value="3">3</option>
                                            <option value="4">4</option>
                                        </Form.Select>
                                    </FloatingLabel>
                                </Col>
                            </Row>
                        </Form.Group>
                        <Form.Group as={Row} className="mb-3" controlId="formDescription">
                            <Form.Label column sm="2">
                                Beschrijving:
                            </Form.Label>
                            <Col sm="10">
                                <Form.Control as="textarea" rows={3} type="text" name="description" value={skill.description} onChange={updateField} required />
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

export default EditSkill