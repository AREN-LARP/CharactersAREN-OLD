import { Routes, Route } from 'react-router-dom';
import PlayerNavBar from './Components/Nav/PlayerNavBar'
import 'bootswatch/dist/darkly/bootstrap.min.css';
import Profile from './Pages/userProfile';
import CharacterOverviewPage from './Pages/CharacterOverviewPage';
import CharacterPage from './Pages/CharacterPage';
import RulePage from './Pages/RulePage';

import React from 'react'

function App() {
  return (
    <div id="app" className="d-flex flex-column h-100">
      <PlayerNavBar/>
      <div className="container flex-grow-1">
        <Routes>
          <Route path="profile" element={<Profile />} />
          <Route path="characters" element={<CharacterOverviewPage />} />
          <Route path="characters/:characterId" element={<CharacterPage />} />
          <Route path="rules" element={<RulePage />} />
          <Route
            path="*"
            element={
              <main style={{ padding: "1rem" }}>
                <p>There's nothing here!</p>
              </main>
            }
          />
        </Routes>
      </div>
    </div>
  );
}

export default App;

