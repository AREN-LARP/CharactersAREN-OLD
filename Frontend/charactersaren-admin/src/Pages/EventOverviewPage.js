import Loading from "../Components/Loading";
import { withAuthenticationRequired } from "@auth0/auth0-react";

const EventOverviewPage = () => {
    return (
        <div className="Container">
            <h1>Events: </h1> 

        </div>
    )
}

export default withAuthenticationRequired(EventOverviewPage, {
    onRedirecting: () => <Loading />,
  });