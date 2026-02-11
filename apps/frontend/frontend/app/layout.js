import "./globals.css";
import Header from "./components/header";
import CustomCalendar from "./components/customCalendar";
import WeatherWidget from "./components/weatherWidget";
import UserProfile from "./components/userProfile";
import SearchBar from "./components/searchBar";

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body>
        <div className="grid-container">
          <div className="grid-header">
            <Header />
          </div>
          <div className="grid-userProfile">
            <UserProfile />
          </div>
          <div className="grid-calendarWeather">
            <CustomCalendar />
            <WeatherWidget />
          </div>
          <div className="grid-searchBar">
            <SearchBar />
          </div>
          <div className="grid-children">
            {children}
          </div>
        </div>
      </body>
    </html>
  );
}
