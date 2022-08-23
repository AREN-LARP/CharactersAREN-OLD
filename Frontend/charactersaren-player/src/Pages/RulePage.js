import { useAuth0, withAuthenticationRequired } from '@auth0/auth0-react';
import Loading from '../Components/Loading';

const Rulepage = () => {
    return <div>
        <h1>Regels</h1>
        <h2>Skill Document</h2>

    </div>
}

export default withAuthenticationRequired(Rulepage, {
    onRedirecting: () => <Loading />,
  });