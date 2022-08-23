import Form from 'react-bootstrap/Form'
import ListGroup from 'react-bootstrap/ListGroup'
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/esm/Container';
import Button from 'react-bootstrap/Button';
import Badge from 'react-bootstrap/Badge'
import PropTypes from 'prop-types'
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { Tab, Tabs } from 'react-bootstrap'
import { useAuth0, withAuthenticationRequired } from '@auth0/auth0-react';
import axios from 'axios';
import Loading from '../Components/Loading';

const CharacterPage = () => {
    let params = useParams();
    let domain = process.env.REACT_APP_BACKEND
    const [key, setKey] = useState('skills');
    const [search, setSearch] = useState('');
    const [character, setCharacter] = useState({ user: {}, skills: []})
    const [skills, setSkills] = useState([]);
    const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();
    useEffect(async () => {
        const accessToken = await getAccessTokenSilently({ audience: `https://characters-aren.azurewebsites.net`, scope: "" });
        let header = { headers: { Authorization: `Bearer ${accessToken}` } }
        axios.get(domain + '/api/characters/' + params.characterId, header).then(response => setCharacter(response.data));
        axios.get(domain + '/api/skills', header).then(
             response => setSkills(response.data.sort((a,b) => {
                 return compare(
                    [compare(a.skillCategory.name, b.skillCategory.name), compare(a.level, b.level)],
                    [compare(b.skillCategory.name, a.skillCategory.name), compare(b.level, a.level)]
                 );
             })));
    }, [])

    const compare = (x, y) => {
        return x > y ? 1 : x < y ? -1 : 0;
    }

    const renderSkill = (skill, index) => {
        return (
            <ListGroup.Item key= {index} as="li" className="d-flex justify-content-between align-items-start">
                <div className="ms-2 me-auto">
                    <div className="fw-bold">{skill.name}</div>
                    {skill.description}
                </div>
                <Badge variant="secondary" pill>
                    {skill.skillCategory.name}
                </Badge>
                <Badge bg="info" pill>
                  Niveau {skill.level}
                </Badge>     
                <Button variant="secondary" onClick={() => moveSkill(skill)} >
                    Move
                </Button>
            </ListGroup.Item>
        );
    }
    const moveSkill = (skill) => {
        if (!!character.skills.includes(skill)) {
            setCharacter({...character, skills: character.skills.filter(s => s !== skill)})
        } else {
            setCharacter({...character, skills: character.skills.concat(skill)})
        }
    }
    const handleChange = (e) => {
        setCharacter({...character, [e.target.name]: e.target.value})
    }
    const handleSubmit = async (event) => {
        const form = event.currentTarget;
        event.preventDefault();

        try {
            const accessToken = await getAccessTokenSilently({ audience: `https://characters-aren.azurewebsites.net`, scope: "" });
            let header = { headers: { Authorization: `Bearer ${accessToken}` } }
            await axios.put(domain + '/api/characters/' + character.id, character, header);
            window.alert("opslaan gelukt!");
        }
        catch {
            window.alert("opslaan mislukt!");
        }
    }

    return (
        <div className="Container">
            <Container>
                <h1>IC: {character.icName}</h1>
                <Row>
                    <Col><h3><small className="text-muted">OC: {character.user.email}</small></h3></Col>
                    <Col md={{ span: 3, offset: 3 }}>
                        <Button variant="primary" type="submit" onClick={handleSubmit}>
                            Submit
                        </Button>
                    </Col>
                </Row>
                <Tabs
                    id="controlled-tab-example"
                    activeKey={key}
                    onSelect={(k) => setKey(k)}
                    className="mb-3"
                >
                    <Tab eventKey="skills" title="Skills">
                        <Row className="mb-3">
                            <input type="text" placeholder="zoeken" name="search" onChange={(e) => setSearch(e.target.value)} />
                            <Form.Group as={Col} controlId="formGridList">
                                <h4>Alle skills</h4>
                                <ListGroup as="ol">
                                    {skills.filter(({id}) => !character.skills.some(s => s.id === id))
                                        .filter(s => s.name.toLowerCase().includes(search.toLowerCase()))
                                        .map(renderSkill)}
                                </ListGroup>
                            </Form.Group>
                            <Form.Group as={Col} controlId="formGridList">
                                <h4>Karakterskills</h4>
                                <ListGroup as="ol">
                                    {character.skills.sort((a,b) => {
                                         return compare(
                                            [compare(a.skillCategory.name, b.skillCategory.name), compare(a.level, b.level)],
                                            [compare(b.skillCategory.name, a.skillCategory.name), compare(b.level, a.level)]
                                         )})
                                        .filter(s => s.name.toLowerCase().includes(search.toLowerCase()))
                                        .map(renderSkill)}
                                </ListGroup>
                            </Form.Group>
                        </Row>
                    </Tab>
                    <Tab eventKey="backstory" title="Backstory">
                        <Form.Group as={Row} controlId="formBackstory">
                        <Form.Control as="textarea" rows={14} name="backstory" placeholder="Karakter Backstory" onChange={handleChange} defaultValue={character.backstory} />
                        </Form.Group>
                    </Tab>
                </Tabs>               
            </Container >
        </div >
    )
}

export default withAuthenticationRequired(CharacterPage, {
    onRedirecting: () => <Loading />,
  });