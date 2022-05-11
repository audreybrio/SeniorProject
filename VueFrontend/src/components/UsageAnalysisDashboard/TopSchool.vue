<template>
    <canvas id="myChart" width="300" height="300"></canvas>

</template>

<script>
    import Chart from 'chart.js/auto';

    export default {
        name: 'TopSchool',
        // Get data from UADMain
        props: {
            name: {
                type: Object,
                required: true
            },
            count: {
                type: Object,
                required: true
            }
        },
        mounted() {
            // Create Graph
            const ctx = document.getElementById('myChart').getContext('2d');
            const myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: this.name,
                    datasets: [{
                        label: '# of Users From Different Universities',
                        data: this.count,
                        backgroundColor: '#267031',
                        borderColor: [
                            'rgba(0, 0, 0, 1)',
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
            myChart;
        }
    }

</script>