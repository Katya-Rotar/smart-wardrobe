import styles from "../styles/eventCard.module.css";

export default function EventCard() {
  return (
    <div className={styles.card}>
      <div className={styles.header}>
        <div className={styles.indicator}></div>
        <h2 className={styles.title}>Gala Dinner</h2>
        <span className={styles.time}>Friday, 7:00 PM</span>
      </div>

      <div className={styles.details}>
        <p>
          <span className={styles.bold}>Dress Code:</span>
          <span className={styles.light}>
            {" "}
            Formal
          </span>
        </p>
        <p>
          <span className={styles.bold}>Planned Outfit:</span>
          <span className={styles.light}>
            {" "}
            Black evening dress, heels, and pearl earrings
          </span>
        </p>
      </div>
    </div>
  );
}
