mergeInto(LibraryManager.library, {
  GeneratePDFWithStats: function (jsonStringPtr) {
    // ? UTF8ToString більше не працює, тому:
    const jsonString = UTF8ToString(jsonStringPtr); // ? Заміна
    const data = JSON.parse(jsonString);

    const { jsPDF } = window.jspdf;
    const doc = new jsPDF();

    const stats = data.Stats;
    const user = data.UserInfo;

    doc.text("Player Tennis Stats", 10, 10);

    let y = 20;

    doc.text(`Username: ${user.Username}`, 10, y); y += 10;
    doc.text(`Country: ${user.Country}`, 10, y); y += 10;
    doc.text(`Level: ${user.Level}`, 10, y); y += 10;

    doc.text(`Games Played: ${stats.GamesPlayed}`, 10, y); y += 10;
    doc.text(`Serves: ${stats.Serves}`, 10, y); y += 10;
    doc.text(`Hits: ${stats.Hits}`, 10, y); y += 10;
    doc.text(`Points: ${stats.Points}`, 10, y); y += 10;

    doc.save("TennisStats.pdf");
  }
});
