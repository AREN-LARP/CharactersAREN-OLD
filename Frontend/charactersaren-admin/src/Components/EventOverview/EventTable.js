import Table from 'react-bootstrap/Table'
import React, { useState, useEffect } from 'react'
import axios from 'axios'
import Button from 'react-bootstrap/Button';
import { useNavigate } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';

const EventTable = () => {
    const [events, setEvents] = useState([])
    const [render, setRender] = useState(<tr></tr>)
    const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();
    let domain = process.env.REACT_APP_BACKEND
    const navigate = useNavigate();

    useEffect(async () => {
        //await axios.get(url).then(json => setCharacters(json.data))
        const accessToken = await getAccessTokenSilently({ audience: `https://characters-aren.azurewebsites.net`, scope: "" });
        let header = { headers: { Authorization: `Bearer ${accessToken}` } }
        const response = await axios.get(domain + '/api/events', header)
        setEvents(response.data)
    }, [])

    useEffect(() => {
        setRender(events.map(RenderEvent));
    }, [events]);

    const deleteEvent = async (e) => {
        const accessToken = await getAccessTokenSilently({ audience: `https://characters-aren.azurewebsites.net`, scope: "" });
        let header = { headers: { Authorization: `Bearer ${accessToken}` } }
        await axios.delete(domain + '/api/events/' + e.target.value, header)
        setEvents(events.filter(event => event.id != e.target.value))
    }

    const RenderEvent = (event, index) => {
        var characterAmount = event.characters.length;
        return (
            <tr key={index}>
                <td>{event.name}</td>
                <td>{event.data}</td>
                <td>{characterAmount}</td>
                <td>
                    <Button onClick={() => navigate("./" + event.id)}>Details</Button>
                    <Button value={event.id} name={index} variant="danger" onClick={deleteEvent}>Delete</Button>
                </td>
            </tr>
        )
    }
    return (
        <Table className="table table-hover" striped bordered>
            <thead>
                <tr>
                    <th>Titel</th>
                    <th>Datum</th>
                    <th>Aantal Spelers</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {render}
            </tbody>
        </Table>
    )
}

export default EventTable