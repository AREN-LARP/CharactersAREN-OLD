import CharacterTable from "../Components/CharacterOverview/Table/CharacterTable";
import AddCharacter from "../Components/CharacterOverview/AddCharacter";
import { withAuthenticationRequired } from "@auth0/auth0-react";
import Loading from "../Components/Loading";
import axios from 'axios';
import { useState, useEffect } from 'react';
import { useAuth0 } from '@auth0/auth0-react';
const CharacterOverviewPage = () => {
    let domain = process.env.REACT_APP_BACKEND
    const [player, setPlayer] = useState('');
    const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();
    const [loading, setLoading] = useState(true);
    
    const getCurrentUser = async () => {
        const accessToken = await getAccessTokenSilently({ audience: `https://characters-aren.azurewebsites.net`, scope: "" });
        let header = { headers: { Authorization: `Bearer ${accessToken}` } }
        console.log(user);
        axios.get(domain + '/api/users/CurrentUser/' + user.sub, header).then(response => setPlayer(response.data));
    }

    useEffect(() => {
        getCurrentUser();
        setLoading(false);
    }, []);

    return (loading ? <Loading /> :
        <div className="Container">
            <h1>Karakters: </h1>
            <AddCharacter userId={player.id} />
            <CharacterTable player={player} />
        </div>
    )
}

export default withAuthenticationRequired(CharacterOverviewPage, {
    onRedirecting: () => <Loading />,
});