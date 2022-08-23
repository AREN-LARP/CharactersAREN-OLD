import Loading from '../Components/Loading.js';
import { withAuthenticationRequired } from "@auth0/auth0-react";
import axios from 'axios';
import AddCategory from '../Components/AddCategory.js'
import AddSkill from '../Components/AddSkill.js'
import { useState, useEffect } from 'react'
import SkillCategoryOverview from '../Components/SkillOverview/SkillCategoryOverview.js'
import { useAuth0 } from '@auth0/auth0-react';

const SkillOverviewPage = () => {
    let domain = process.env.REACT_APP_BACKEND;
    const [categories, setCategories] = useState([]);
    const [update, setUpdate] = useState(0);
    const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();
    const [isLoaded, setIsLoaded] = useState(false);
    const [error, setError] = useState(null);

    const loadData = async () => {
        const accessToken = await getAccessTokenSilently({ audience: `https://characters-aren.azurewebsites.net`, scope: "" });
        let header = { headers: { Authorization: `Bearer ${accessToken}` } }
        axios.get(domain + '/api/SkillCategories', header).then(response => {
            setCategories(response.data)
        });
    };


    useEffect(() => {
        setCategories([]);
        loadData();
    }, [update])


    return (
        <div>
            {categories.length === 0 ? (<div></div>) : (
                <div className="Container">
                <h1>Skills:</h1>
                <AddSkill
                    labelName="Categorie"
                    categories={categories}
                    reload={() => setUpdate(update+1)} />
                <AddCategory reload={() => setUpdate(update+1)}/>
                <SkillCategoryOverview categories={categories} reload={() => setUpdate(update+1)} />
            </div>
            )}
        </div>
    )
}

export default withAuthenticationRequired(SkillOverviewPage, {
    onRedirecting: () => <Loading />,
});