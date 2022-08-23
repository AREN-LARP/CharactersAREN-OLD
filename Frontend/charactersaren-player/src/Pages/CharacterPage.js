import Container from 'react-bootstrap/esm/Container';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { Tab, Tabs, Alert, Col,Row, ListGroup, Form, Badge} from 'react-bootstrap'
import { useAuth0, withAuthenticationRequired } from '@auth0/auth0-react';
import axios from 'axios';
import Loading from '../Components/Loading';

const CharacterPage = (props) => {
    let params = useParams();
    let domain = process.env.REACT_APP_BACKEND
    const [key, setKey] = useState('skills');
    const [search, setSearch] = useState('');
    const [character, setCharacter] = useState({ user: {}, skills: [] })
    const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();
    useEffect(async () => {
        const accessToken = await getAccessTokenSilently({ audience: `https://characters-aren.azurewebsites.net`, scope: "" });
        let header = { headers: { Authorization: `Bearer ${accessToken}` } }
        axios.get(domain + '/api/characters/' + params.characterId, header).then(response => setCharacter(response.data));
    }, [])

    const compare = (x, y) => {
        return x > y ? 1 : x < y ? -1 : 0;
    }

    const renderSkill = (skill, index) => {
        return (
            <ListGroup.Item key={index} as="li" className="d-flex justify-content-between align-items-start">
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
            </ListGroup.Item>
        );
    }

    return (
        <div className="Container">
            <Container>
                <h1>IC: {character.icName}</h1>
                <Row>
                    <Col><h3><small className="text-muted">OC: {character.user.email}</small></h3></Col>
                </Row>
                <Alert variant="info">
                    <Alert.Heading>Hey, nice to see you</Alert.Heading>
                    <p>
                        Op het moment kunnen enkel admins Karakters aanpassen. 
                        Nog eventjes geduld.
                    </p>
                </Alert>
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
                                <h4>Karakterskills</h4>
                                <ListGroup as="ol">
                                    {character.skills.sort((a, b) => {
                                        return compare(
                                            [compare(a.skillCategory.name, b.skillCategory.name), compare(a.level, b.level)],
                                            [compare(b.skillCategory.name, a.skillCategory.name), compare(b.level, a.level)]
                                        )
                                    })
                                        .filter(s => s.name.toLowerCase().includes(search.toLowerCase()))
                                        .map(renderSkill)}
                                </ListGroup>
                            </Form.Group>
                        </Row>
                    </Tab>
                    <Tab eventKey="backstory" title="Backstory">
                        <Form.Group as={Row} controlId="formBackstory">
                            <Form.Control as="textarea" disabled rows={14} placeholder="Karakter Backstory" defaultValue={character.backstory} />
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