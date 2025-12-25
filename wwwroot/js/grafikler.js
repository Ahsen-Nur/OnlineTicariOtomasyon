$(document).ready(function () {

    loadPie("/Grafik/CariSehirDagilim", "cariSehirChart");
    loadPie("/Grafik/UrunMarkaDagilim", "urunMarkaChart");

    loadLine("/Grafik/GunlukSatis", "gunlukSatisChart");

    loadBar("/Grafik/EnCokSatanUrunler", "enCokSatanChart");
    loadBar("/Grafik/CariHarcama", "cariHarcamaChart");

});



function loadPie(url, canvasId) {
    $.get(url)
        .done(function (data) {

            if (!data || data.length === 0) {
                console.warn("Veri boş:", url);
                return;
            }

            new Chart(document.getElementById(canvasId), {
                type: 'pie',
                data: {
                    labels: data.map(x => x.label),
                    datasets: [{
                        data: data.map(x => x.value),
                        backgroundColor: [
                            "#4e73df", "#1cc88a", "#36b9cc",
                            "#f6c23e", "#e74a3b", "#858796"
                        ]
                    }]
                }
            });

        })
        .fail(function (err) {
            console.error("AJAX HATA:", url, err.status);
        });
}


function loadBar(url, canvasId) {
    $.get(url, function (data) {

        new Chart(document.getElementById(canvasId), {
            type: 'bar',
            data: {
                labels: data.map(x => x.label),
                datasets: [{
                    data: data.map(x => x.value),
                    backgroundColor: "#4e73df"
                }]
            }
        });

    });
}

function loadLine(url, canvasId) {
    $.get(url, function (data) {

        new Chart(document.getElementById(canvasId), {
            type: 'line',
            data: {
                labels: data.map(x => x.label),
                datasets: [{
                    data: data.map(x => x.value),
                    borderColor: "#1cc88a",
                    fill: false,
                    tension: 0.3
                }]
            }
        });

    });
}
