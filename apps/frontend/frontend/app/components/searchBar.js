import styles from '../styles/searchBar.module.css'

const SearchBar = () => {
   return (
     <div className={styles.container}>
       <img className={styles.icon} src="/search.svg"></img>
       <input type="text" placeholder="Search here..." className={styles.input} />
     </div>
   );
 };
 
 export default SearchBar;