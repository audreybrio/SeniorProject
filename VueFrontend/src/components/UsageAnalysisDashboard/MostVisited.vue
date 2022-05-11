<template>
    <canvas id="myChart" width="300" height="300"></canvas>

</template>

<script>
    import Chart from 'chart.js/auto';

    export default {
        name: 'MostVisited',
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
                type: 'bar',
                data: {
                    labels: this.name,
                    datasets: [{
                        label: '# of Views of Page',
                        data: this.count,
                        backgroundColor: '#79acf8',
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