import Table from 'react-bootstrap/Table'
import React, { useState, useEffect } from 'react'
import axios from 'axios'
import Button from 'react-bootstrap/Button';
import { useNavigate } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';

const CharacterTable = (props) => {
    const [characters, setCharacters] = useState([])
    const [render, setRender] = useState(<tr></tr>)
    const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();
    let domain = process.env.REACT_APP_BACKEND
    const navigate = useNavigate();

    const getCharacters = async () => {
        const accessToken = await getAccessTokenSilently({ audience: `https://characters-aren.azurewebsites.net`, scope: "" });
        let header = { headers: { Authorization: `Bearer ${accessToken}` } };
        const response = await axios.get(domain + '/api/characters/usercharacters/' + props.player.id, header);
        setCharacters(response.data);
    }

    useEffect(() => {
        if(props.player !== ''){
            getCharacters();
        }
    }, [props])

    useEffect(() => {
        setRender(characters.map(RenderCharacter));
    }, [characters]);

    const showStatus = (status) => {
        switch (status) {
            case 0:
                return "New"
            case 1:
                return "Active"
            case 2:
                return "In review"
            case 3:
                return "Archived"     
            case 4:
                return "Denied"         
            case 5:
                return "Approved"            
            default:
                break;
        }
    }   

    const RenderCharacter = (character, index) => {
        return (
            <tr key={index}>
                <td>{character.icName}</td>
                <td>{showStatus(character.status)}</td>
                <td>
                    <Button onClick={() => navigate("./" + character.id)}>Details</Button>                   
                </td>
            </tr>
        )
    }
    return (
        <Table className="table table-hover" striped bordered>
            <thead>
                <tr>
                    <th>IC Naam</th>
                    <th>Status</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {render}
            </tbody>
        </Table>
    )
}

export default CharacterTable