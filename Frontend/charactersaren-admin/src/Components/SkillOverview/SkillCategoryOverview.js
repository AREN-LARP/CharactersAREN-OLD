import PropTypes from 'prop-types'
import Container from 'react-bootstrap/esm/Container';
import Accordion from 'react-bootstrap/Accordion'
import Button from 'react-bootstrap/Button'
import { useState, useEffect } from 'react'
import axios from 'axios'
import SkillsOverview from './SkillsOverview';
import { useAuth0 } from '@auth0/auth0-react';

function SkillCategoryOverview(props) {
    let domain = process.env.REACT_APP_BACKEND;
    const [skills, setSkills] = useState([]);
    const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();


    const getSkills = async () => {
        const accessToken = await getAccessTokenSilently({ audience: `https://characters-aren.azurewebsites.net`, scope: "" });
        let header = { headers: { Authorization: `Bearer ${accessToken}` } }
        axios.get(domain + '/api/Skills/', header).then(response => setSkills(response.data))
    };

    const getSkillsByCategory = (categoryId) => {
        return skills.filter(skill => skill.skillCategoryId == categoryId).sort((a, b) => a.level - b.level);
    }


    useEffect(() => {
        getSkills();
    }, [])

    const renderCategory = (category, index) => {
        return (
            <Accordion.Item eventKey={category} key={index}>
                <Accordion.Header>{category.name}</Accordion.Header>
                <Accordion.Body>
                    <SkillsOverview skills={getSkillsByCategory(category.id)} categories={props.categories} reload={() => props.reload()}/>
                </Accordion.Body>
            </Accordion.Item>
        );
    }

    return (
        <Container>
            <Accordion>
                {skills.length === 0 ? (<div></div>) : props.categories.map(renderCategory)}
            </Accordion>
        </Container>
    );
}

export default SkillCategoryOverview