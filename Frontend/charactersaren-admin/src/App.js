import 'bootswatch/dist/darkly/bootstrap.min.css';
import CharacterOverviewPage from './Pages/CharacterOverviewPage';
import SkillOverviewPage from './Pages/SkillOverViewPage';
import AdminNavBar from './Components/AdminNavBar';
import { Routes, Route } from 'react-router-dom';
import Profile from './Components/userProfile';
import CharacterPage from './Pages/CharacterPage';

function App() {
  return (
    <div id="app" className="d-flex flex-column h-100">
      <AdminNavBar />
      <div className="container flex-grow-1">
        <Routes>
          <Route path="profile" element={<Profile />} />
          <Route path="characters" element={<CharacterOverviewPage />} />
          <Route path="characters/:characterId" element={<CharacterPage />} />
          <Route path="skills" element={<SkillOverviewPage />} />
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
