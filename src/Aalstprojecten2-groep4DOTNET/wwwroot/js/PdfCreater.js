$(document)
    .ready(function () {
        var canvas;
        var context;
        var knop;
        $(".downloadIcoonKnop")
            .click(function () {
                knop = $(this);
                canvas = document.getElementById("canvas");
                context = canvas.getContext('2d');
                schrijfTitel();
                tekenHeaders();
                tekenRij1();
                tekenRij2();
                tekenRij3();
                tekenRij4();
                tekenRij5();
                tekenRij6();
                tekenRij7();
                tekenRij8();
                tekenRij9();
                tekenRij10();
                tekenRij11();
                tekenSubtotaalRij();
                tekenEindResultaat();

                var imgData = canvas.toDataURL('image/png');
                var doc = new jsPDF('p', 'mm');
                doc.addImage(imgData, 'PNG', 10, 10);
                doc.save("Analyse-" + knop.parent().siblings(".datadiv").data("organisatie") + "-" + knop.parent().siblings(".datadiv").data("afdeling") + ".pdf");
            });
        function schrijfTitel() {
            context.font = "20px Helvetica";
            context.fillStyle = "#000000";
            context.textAlign = "center";
            context.fillText(knop.parent().siblings(".datadiv").data("organisatie") + " - " + knop.parent().siblings(".datadiv").data("afdeling") + " (" + knop.parent().siblings(".datadiv").data("locatie") + ", " + knop.parent().siblings(".datadiv").data("straat") + ", " + knop.parent().siblings(".datadiv").data("nummer") + ")", 350, 20);
        }
        function tekenHeaders() {
            context.beginPath();
            context.rect(0, 45, 238, 60);
            context.fillStyle = "#008B8B";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(462, 45, 238, 60);
            context.fillStyle = "#A52A2A";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.font = "20px Helvetica";
            context.fillStyle = "#FFFFFF";
            context.textAlign = "center";
            context.fillText("Baten", 119, 70);
            context.fillText("Kosten", 581, 70);
        }
        function tekenRij1() {
            context.beginPath();
            context.rect(0, 105, 238, 60);
            context.fillStyle = "#EAF1F1";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(238, 105, 112, 60);
            context.fillStyle = "#F5F5F5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(350, 105, 112, 60);
            context.fillStyle = "#F5F5F5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(462, 105, 238, 60);
            context.fillStyle = "#F2ECEC";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.font = "13px Helvetica";
            context.fillStyle = "#000000";
            context.textAlign = "left";
            context.fillText("Totale loonkostsubsidies", 10, 128);
            context.fillText("(VOP, IBO en doelgroepvermindering)", 10, 148);
            context.textAlign = "right";
            context.fillText("Loonkosten medewerkers met", 690, 128);
            context.fillText("grote afstand tot arbeidsmarkt", 690, 148);
            context.textAlign = "center";
            context.fillText(knop.parent().siblings(".datadiv").data("baat1"), 294, 138);
            context.fillText(knop.parent().siblings(".datadiv").data("kost1"), 406, 138);
        }
        function tekenRij2() {
            context.beginPath();
            context.rect(0, 165, 238, 60);
            context.fillStyle = "#E1EFEF";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(238, 165, 112, 60);
            context.fillStyle = "#F9F9F9";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(350, 165, 112, 60);
            context.fillStyle = "#F9F9F9";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(462, 165, 238, 60);
            context.fillStyle = "#F1E5E5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.font = "13px Helvetica";
            context.fillStyle = "#000000";
            context.textAlign = "left";
            context.fillText("Besparing reguliere medew.", 10, 188);
            context.fillText("op hetzelfde niveau", 10, 208);
            context.textAlign = "right";
            context.fillText("Kosten enclave of onderaanneming", 690, 198);
            context.textAlign = "center";
            context.fillText(knop.parent().siblings(".datadiv").data("baat3"), 294, 198);
            context.fillText(knop.parent().siblings(".datadiv").data("kost1punt1"), 406, 198);
        }
        function tekenRij3() {
            context.beginPath();
            context.rect(0, 225, 238, 60);
            context.fillStyle = "#EAF1F1";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(238, 225, 112, 60);
            context.fillStyle = "#F5F5F5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(350, 225, 112, 60);
            context.fillStyle = "#F5F5F5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(462, 225, 238, 60);
            context.fillStyle = "#F2ECEC";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.font = "13px Helvetica";
            context.fillStyle = "#000000";
            context.textAlign = "left";
            context.fillText("Besparing reguliere medew.", 10, 248);
            context.fillText("op hoger niveau", 10, 268);
            context.textAlign = "right";
            context.fillText("Extra kosten werkkleding", 690, 248);
            context.fillText("e.a. personeelskosten", 690, 268);
            context.textAlign = "center";
            context.fillText(knop.parent().siblings(".datadiv").data("baat4"), 294, 258);
            context.fillText(knop.parent().siblings(".datadiv").data("kost3"), 406, 258);
        }
        function tekenRij4() {
            context.beginPath();
            context.rect(0, 285, 238, 60);
            context.fillStyle = "#E1EFEF";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(238, 285, 112, 60);
            context.fillStyle = "#F9F9F9";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(350, 285, 112, 60);
            context.fillStyle = "#F9F9F9";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(462, 285, 238, 60);
            context.fillStyle = "#F1E5E5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.font = "13px Helvetica";
            context.fillStyle = "#000000";
            context.textAlign = "left";
            context.fillText("Besparing (extra) uitzendkrachten", 10, 318);
            context.textAlign = "right";
            context.fillText("Extra kosten voor aanpassingen", 690, 308);
            context.fillText("werkomgeving/aangepast gereedschap", 690, 328);
            context.textAlign = "center";
            context.fillText(knop.parent().siblings(".datadiv").data("baat5"), 294, 318);
            context.fillText(knop.parent().siblings(".datadiv").data("kost4"), 406, 318);
        }
        function tekenRij5() {
            context.beginPath();
            context.rect(0, 345, 238, 60);
            context.fillStyle = "#EAF1F1";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(238, 345, 112, 60);
            context.fillStyle = "#F5F5F5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(350, 345, 112, 60);
            context.fillStyle = "#F5F5F5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(462, 345, 238, 60);
            context.fillStyle = "#F2ECEC";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.font = "13px Helvetica";
            context.fillStyle = "#000000";
            context.textAlign = "left";
            context.fillText("Besparing op outsourcing", 10, 378);
            context.textAlign = "right";
            context.fillText("Voorbereiding start medewerker met", 690, 368);
            context.fillText("grote afstand tot de arbeidsmarkt", 690, 388);
            context.textAlign = "center";
            context.fillText(knop.parent().siblings(".datadiv").data("baat9"), 294, 378);
            context.fillText(knop.parent().siblings(".datadiv").data("kost2"), 406, 378);
        }
        function tekenRij6() {
            context.beginPath();
            context.rect(0, 405, 238, 60);
            context.fillStyle = "#E1EFEF";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(238, 405, 112, 60);
            context.fillStyle = "#F9F9F9";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(350, 405, 112, 60);
            context.fillStyle = "#F9F9F9";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(462, 405, 238, 60);
            context.fillStyle = "#F1E5E5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.font = "13px Helvetica";
            context.fillStyle = "#000000";
            context.textAlign = "left";
            context.fillText("Besparing op overuren", 10, 438);
            context.textAlign = "right";
            context.fillText("Extra kosten administratie", 690, 428);
            context.fillText("en begeleiding", 690, 448);
            context.textAlign = "center";
            context.fillText(knop.parent().siblings(".datadiv").data("baat8"), 294, 438);
            context.fillText(knop.parent().siblings(".datadiv").data("kost6"), 406, 438);
        }
        function tekenRij7() {
            context.beginPath();
            context.rect(0, 465, 238, 60);
            context.fillStyle = "#EAF1F1";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(238, 465, 112, 60);
            context.fillStyle = "#F5F5F5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(350, 465, 112, 60);
            context.fillStyle = "#F5F5F5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(462, 465, 238, 60);
            context.fillStyle = "#F2ECEC";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.font = "13px Helvetica";
            context.fillStyle = "#000000";
            context.textAlign = "left";
            context.fillText("Logistieke besparing", 10, 498);
            context.textAlign = "right";
            context.fillText("Extra kosten opleiding", 690, 498);
            context.textAlign = "center";
            context.fillText(knop.parent().siblings(".datadiv").data("baat10"), 294, 498);
            context.fillText(knop.parent().siblings(".datadiv").data("kost5"), 406, 498);
        }
        function tekenRij8() {
            context.beginPath();
            context.rect(0, 525, 238, 60);
            context.fillStyle = "#E1EFEF";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(238, 525, 112, 60);
            context.fillStyle = "#F9F9F9";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(350, 525, 112, 60);
            context.fillStyle = "#F9F9F9";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(462, 525, 238, 60);
            context.fillStyle = "#F1E5E5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.font = "13px Helvetica";
            context.fillStyle = "#000000";
            context.textAlign = "left";
            context.fillText("Inperking omzetverlies", 10, 558);
            context.textAlign = "right";
            context.fillText("Andere kosten", 690, 558);
            context.textAlign = "center";
            context.fillText(knop.parent().siblings(".datadiv").data("baat6"), 294, 558);
            context.fillText(knop.parent().siblings(".datadiv").data("kost7"), 406, 558);
        }
        function tekenRij9() {
            context.beginPath();
            context.rect(0, 585, 238, 60);
            context.fillStyle = "#EAF1F1";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(238, 585, 112, 60);
            context.fillStyle = "#F5F5F5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(350, 585, 112, 60);
            context.fillStyle = "#F5F5F5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(462, 585, 238, 60);
            context.fillStyle = "#F2ECEC";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.font = "13px Helvetica";
            context.fillStyle = "#000000";
            context.textAlign = "left";
            context.fillText("Productiviteitswinst", 10, 618);
            context.textAlign = "center";
            context.fillText(knop.parent().siblings(".datadiv").data("baat7"), 294, 618);
        }
        function tekenRij10() {
            context.beginPath();
            context.rect(0, 645, 238, 80);
            context.fillStyle = "#E1EFEF";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(238, 645, 112, 80);
            context.fillStyle = "#F9F9F9";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(350, 645, 112, 80);
            context.fillStyle = "#F9F9F9";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(462, 645, 238, 80);
            context.fillStyle = "#F1E5E5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.font = "13px Helvetica";
            context.fillStyle = "#000000";
            context.textAlign = "left";
            context.fillText("Tegemoetkoming in de kosten", 10, 668);
            context.fillText("voor aanpassingen werkomgeving", 10, 688);
            context.fillText("en aangepast gereedschap", 10, 708);
            context.textAlign = "center";
            context.fillText(knop.parent().siblings(".datadiv").data("baat2"), 294, 688);
        }
        function tekenRij11() {
            context.beginPath();
            context.rect(0, 725, 238, 60);
            context.fillStyle = "#EAF1F1";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(238, 725, 112, 60);
            context.fillStyle = "#F5F5F5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(350, 725, 112, 60);
            context.fillStyle = "#F5F5F5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(462, 725, 238, 60);
            context.fillStyle = "#F2ECEC";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.font = "13px Helvetica";
            context.fillStyle = "#000000";
            context.textAlign = "left";
            context.fillText("Andere besparingen", 10, 758);
            context.textAlign = "center";
            context.fillText(knop.parent().siblings(".datadiv").data("baat11"), 294, 758);
        }
        function tekenSubtotaalRij() {
            context.beginPath();
            context.rect(0, 805, 238, 60);
            context.fillStyle = "#E1EFEF";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(238, 805, 112, 60);
            context.fillStyle = "#F9F9F9";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(350, 805, 112, 60);
            context.fillStyle = "#F9F9F9";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.beginPath();
            context.rect(462, 805, 238, 60);
            context.fillStyle = "#F1E5E5";
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            context.font = "bold 13px Helvetica";
            context.fillStyle = "#000000";
            context.textAlign = "left";
            context.fillText("Totaal Baten", 10, 838);
            context.textAlign = "right";
            context.fillText("Totaal Kosten", 690, 838);
            context.textAlign = "center";
            context.fillText(knop.parent().siblings(".datadiv").data("subtotaalbaten"), 294, 838);
            context.fillText(knop.parent().siblings(".datadiv").data("subtotaalkosten"), 406, 838);
        }
        function tekenEindResultaat() {
            context.beginPath();
            context.rect(238, 865, 224, 60);
            var tekst = knop.parent().siblings(".datadiv").data("nettoresultaat");
            var nettoResultaat = parseFloat(tekst.replace("€", "").replace(",","").split(" ").join("").split(".").join(""));
            context.font = "bold 13px Helvetica";
            if (nettoResultaat === 0) {
                context.fillStyle = "#D3D3D3";
            }else if (nettoResultaat > 0) {
                context.fillStyle = "#008B8B";
            } else {
                context.fillStyle = "#A52A2A";
                tekst = tekst.replace("-", "");
            }
            
            context.fill();
            context.lineWidth = 2;
            context.strokeStyle = "#DDDDDD";
            context.stroke();

            
            context.fillStyle = "#FFFFFF";
            context.textAlign = "center";
            context.fillText(tekst, 350, 898);
        }
    });