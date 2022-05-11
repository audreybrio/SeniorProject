<template>
    <canvas id="myChart" width="1000" height="300"></canvas>

</template>

<script>
    import Chart from 'chart.js/auto';

    export default {
        name: 'NumLogins',
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
            // Create graph
            const ctx = document.getElementById('myChart').getContext('2d');
            const myChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: this.name,
                    datasets: [{
                        label: '# of Login Per Day',
                        data: this.count,
                        backgroundColor: '#c399c9',
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