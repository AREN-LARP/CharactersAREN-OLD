import Col from 'react-bootstrap/esm/Col';
import Row from 'react-bootstrap/esm/Row';
import Container from 'react-bootstrap/esm/Container';
import Accordion from 'react-bootstrap/Accordion';
import Badge from 'react-bootstrap/Badge'
import Button from 'react-bootstrap/Button'
import EditSkill from '../EditSkill';
import { useState, useEffect } from 'react'
import axios from 'axios';
import { useAuth0 } from '@auth0/auth0-react';

const SkillsOverview = (props) => {
    let domain = process.env.REACT_APP_BACKEND;
    const [render, setRender] = useState(<div></div>)
    const [skills, setSkills] = useState(props.skills)
    const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();
    const deleteSkill = async (e) => {
        const accessToken = await getAccessTokenSilently({ audience: `https://characters-aren.azurewebsites.net`, scope: "" });
            let header = { headers: { Authorization: `Bearer ${accessToken}` } }
        await axios.delete(domain + '/api/skills/' + e.target.value, header)
        setSkills(skills.filter(skill => skill.id != e.target.value))
    }

    useEffect(() => {
        setRender(
            <Container>
                <Accordion>
                    {skills.map(renderSkills)}
                </Accordion>
            </Container>);
    }, [skills]);

    const renderSkills = (skill, index) => {
        return (
            <Accordion.Item eventKey={skill.id} key={index}>
                <Accordion.Header> <Col sm={11}>{skill.name}</Col> <Col sm={1}><Badge pill className="badge bg-primary">level {skill.level}</Badge></Col> </Accordion.Header>
                <Accordion.Body>
                    <Row>
                    <Col sm={9}>
                        <p>{skill.description}</p>                 
                    </Col>
                    <Col sm={3}>
                    <EditSkill
                        {...skill}
                        categories={props.categories}
                        reload={() => props.reload()}
                    /> {' '}                 
                    <Button value={skill.id} name={index} variant="danger" onClick={deleteSkill}>Delete</Button>
                    </Col>
                    </Row>
                </Accordion.Body>
            </Accordion.Item>
        );
    }

    return render;
}

export default SkillsOverview