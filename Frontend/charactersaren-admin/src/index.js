import React from 'react';
import ReactDOM from 'react-dom';
import 'bootswatch/dist/darkly/bootstrap.min.css';
import { BrowserRouter } from 'react-router-dom';
import './index.css';
import reportWebVitals from './reportWebVitals';
import { Auth0Provider } from "@auth0/auth0-react";
import App from './App'
ReactDOM.render(
  <BrowserRouter>
    <Auth0Provider
      domain="dev-8yft46n8.eu.auth0.com"
      clientId="GCq2MWMtFO6c3e4B4UGS4zNqQzztIMqo"
      redirectUri={window.location.origin}
      audience="https://characters-aren.azurewebsites.net"
      scope="read:current_user update:current_user_metadata create:character read:character"
    >
      <App />
    </Auth0Provider>
  </BrowserRouter>,
  document.getElementById("root")
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
