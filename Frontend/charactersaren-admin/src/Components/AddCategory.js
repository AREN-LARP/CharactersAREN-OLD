import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import { useEffect } from 'react';
import axios from 'axios';
import { useAuth0 } from '@auth0/auth0-react';
import { useState } from 'react';

function AddCategory(props) {
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const [catName, setCategory] = useState('');
  const category = { name: catName.toString() }
  const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();

  let domain = process.env.REACT_APP_BACKEND
  const handleSubmit = async (event) => {
    event.preventDefault();
    const accessToken = await getAccessTokenSilently({ audience: `https://characters-aren.azurewebsites.net`, scope: "" });
    let header = { headers: { Authorization: `Bearer ${accessToken}` } }
    await axios.post(domain + '/api/SkillCategories', category, header);
    handleClose();
    props.reload();
  }


  return (
    <>
      <Button variant="primary" onClick={handleShow}>
        Categorie Toevoegen
      </Button>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Nieuwe Categorie</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={handleSubmit} id='frm-add-category'>
            <Form.Group as={Row} className="mb-3" controlId="formPlaintextPassword">
              <Form.Label column sm="2">
                Categorie:
              </Form.Label>
              <Col sm="10">
                <Form.Control type="text" placeholder="Categorie" value={catName}
                  onChange={(e) => setCategory(e.target.value)} />
              </Col>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="primary" form='frm-add-category' type="submit">
            Save Changes
          </Button>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default AddCategory