import CharacterTable from "../Components/CharacterOverview/Table/CharacterTable";
import AddCharacter from "../Components/CharacterOverview/AddCharacter";
import { withAuthenticationRequired } from "@auth0/auth0-react";
import Loading from "../Components/Loading";
const CharacterOverviewPage = () => {
    return (
        <div className="Container">
            <h1>Karakters: </h1> 
            <AddCharacter />          
            <CharacterTable />
        </div>
    )
}

export default withAuthenticationRequired(CharacterOverviewPage, {
    onRedirecting: () => <Loading />,
  });