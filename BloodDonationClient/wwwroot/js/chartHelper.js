// File: wwwroot/js/chartHelper.js

window.chartHelper = {
    createBarChart: function (canvasId, labels, data) {
        var ctx = document.getElementById(canvasId)?.getContext('2d');
        if (!ctx) {
            console.error(`Canvas element with ID '${canvasId}' not found.`);
            return;
        }
        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Blood Variants Collected',
                    data: data,
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.5)',    // A+
                        'rgba(54, 162, 235, 0.5)',    // B+
                        'rgba(255, 206, 86, 0.5)',    // AB+
                        'rgba(75, 192, 192, 0.5)',    // O+
                        'rgba(153, 102, 255, 0.5)',   // A-
                        'rgba(255, 159, 64, 0.5)',    // B-
                        'rgba(199, 199, 199, 0.5)',   // AB-
                        'rgba(83, 102, 255, 0.5)'     // O-
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)',      // A+
                        'rgba(54, 162, 235, 1)',      // B+
                        'rgba(255, 206, 86, 1)',      // AB+
                        'rgba(75, 192, 192, 1)',      // O+
                        'rgba(153, 102, 255, 1)',     // A-
                        'rgba(255, 159, 64, 1)',      // B-
                        'rgba(199, 199, 199, 1)',     // AB-
                        'rgba(83, 102, 255, 1)'       // O-
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        display: false // Hide legend since label is descriptive
                    },
                    title: {
                        display: true,
                        text: 'Blood Variants Collected'
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            precision: 0
                        },
                        title: {
                            display: true,
                            text: 'Number of Units'
                        }
                    },
                    x: {
                        title: {
                            display: true,
                            text: 'Blood Types'
                        }
                    }
                }
            }
        });
    }
};
