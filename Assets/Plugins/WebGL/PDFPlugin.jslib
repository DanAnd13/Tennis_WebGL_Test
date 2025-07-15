mergeInto(LibraryManager.library, {
  GeneratePDFWithStats: function (jsonStringPtr) {
    const jsonString = UTF8ToString(jsonStringPtr);
    const data = JSON.parse(jsonString);

    const { jsPDF } = window.jspdf;
    const doc = new jsPDF();

    const stats = data.Stats;
    const user = data.UserInfo;

    // --- ������ ������� �� ������� ---
    const pageWidth = doc.internal.pageSize.getWidth();

    // --- ��������� ��������� (�� ������, � ������) ---
    doc.setFillColor(30, 144, 255); // ���� ��� (DodgerBlue)
    doc.rect(0, 10, pageWidth, 15, 'F'); // x, y, width, height, fill

    doc.setTextColor(255, 255, 255);
    doc.setFontSize(20);
    doc.setFont("helvetica", "bold");
    doc.text("Player Tennis Statistics", pageWidth / 2, 20, { align: "center" });

    // --- ���������� ����������� (�� ����� ����) ---
    let y = 40;
    doc.setTextColor(0, 0, 0);
    doc.setFontSize(14);
    doc.setFont("helvetica", "normal");

    doc.text(`Username: ${user.Username}`, 10, y); y += 10;
    doc.text(`Country: ${user.Country}`, 10, y); y += 15;

    // --- ��������� "������ ����������" (�� ������ � ������) ---
    doc.setFillColor(220, 220, 220); // �����-���� ���
    const headerHeight = 12;
    doc.rect(pageWidth/4, y - 10, pageWidth/2, headerHeight, 'F');
    doc.setTextColor(0, 0, 0);
    doc.setFontSize(16);
    doc.setFont("helvetica", "bold");
    doc.text("Game Statistics", pageWidth / 2, y, { align: "center" });
    y += 20;

    // --- ���������� (�� ����� ����) ---
    doc.setFontSize(12);
    doc.setFont("helvetica", "normal");
    doc.text(`Games Played: ${stats.GamesPlayed}`, 10, y); y += 10;
    doc.text(`Serves: ${stats.Serves}`, 10, y); y += 10;
    doc.text(`Hits: ${stats.Hits}`, 10, y); y += 10;
    doc.text(`Points: ${stats.Points}`, 10, y); y += 20;

    // --- г���� ������ (Level) ---
    doc.setFontSize(14);
    doc.setFont("helvetica", "bolditalic");
    doc.text(`Level: ${user.Level}`, 10, y);

    // --- ���������� ---
    doc.save("TennisStats.pdf");
  }
});
