import { PlayerNavBarData } from './PlayerNavBarData';
import LoginButton from '../Authentication/loginButton';
import Navbar from 'react-bootstrap/Navbar';
import Nav from 'react-bootstrap/Nav';
import Container from 'react-bootstrap/Container';
import { LinkContainer } from 'react-router-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faDiscord, faFacebook } from '@fortawesome/free-brands-svg-icons';
import { Outlet } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react'

function PlayerNavBar() {
    const { isAuthenticated } = useAuth0();
    return (
        <div>
            <Navbar collapseOnSelect expand="lg" className="bg-primary navbar-dark">
                <Container>

                    <Navbar.Brand href="/">
                        <img
                            alt=""
                            src="AREN_LARP_FINAL.png"
                            width="45"
                            height="45"
                            className="d-inline-block"
                        />{' '}AREN LARP Character Manager</Navbar.Brand>
                    <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                    <Navbar.Collapse id="responsive-navbar-nav">
                        {isAuthenticated &&
                            <Nav className="me-auto">
                                {PlayerNavBarData.map((val, key) => {
                                    return (
                                        <LinkContainer to={`${val.link}`} >
                                            <Nav.Link key={key}>
                                                <div id="title">{val.title}</div>
                                            </Nav.Link>
                                        </LinkContainer>
                                    )
                                })}
                            </Nav>
                        }
                        <Nav>
                            <Nav.Link href="#disc">
                                <FontAwesomeIcon icon={faDiscord} />
                            </Nav.Link>
                            <Nav.Link href="#deets">
                                <FontAwesomeIcon icon={faFacebook} />
                            </Nav.Link>
                            <LoginButton />
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
            <Outlet />
        </div >
    )
}

export default PlayerNavBar
