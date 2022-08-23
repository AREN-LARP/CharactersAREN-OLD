import PropTypes from 'prop-types'
import Container from 'react-bootstrap/esm/Container';
import AdminNavBar from '../Components/AdminNavBar'
import Profile from '../Components/userProfile';

const AdminLandingPage = () => {
    return (
        <div className="Container">
            <Container>
                <AdminNavBar />
                <Profile />
            </Container>

        </div>
    )
}

export default AdminLandingPage