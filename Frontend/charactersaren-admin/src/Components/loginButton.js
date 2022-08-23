import React from "react";
import { useAuth0 } from "@auth0/auth0-react";

const LoginButton = () => {
  const { isAuthenticated, loginWithRedirect, logout, user } = useAuth0();

  return (
    isAuthenticated ?     <button type="button" className="btn btn-secondary" onClick={() => logout({ returnTo: window.location.origin })}>Log Out</button> 
                    :     <button type="button" className="btn btn-secondary" onClick={() => loginWithRedirect()}>Log In</button>
  );
};

export default LoginButton;