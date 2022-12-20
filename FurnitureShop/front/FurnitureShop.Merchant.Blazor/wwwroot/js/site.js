var barChartData = {
    type: 'bar',
    globals: {
        fontFamily: 'Montserrat',
        fontColor: 'var(--darkFontColor)',
        fontWeight: 'normal'
    },
    title: {
        text: 'History',
        align: 'left',
        marginTop: '8px',
        marginLeft: '32px',
        fontWeight: 'normal',
        shadow: false,
        rules: {

        }
    },
    legend: {
        marginTop: '16px',
        borderWidth: 0,
        layout: '1',
        marker: {
            type: 'circle'
        }
    },
    plotarea: {
        margin: '80px 80px 80px 80px'
    },
    plot: {
        barWidth: '8px',
        borderRadius: 4
    },
    scaleX: {
        lineWidth: 0,
        tick: {
            visible: false
        },
        labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Nov', 'Dec']
    },
    scaleY: {
        lineWidth: 0,
        tick: {
            visible: false
        },
        guide: {
            lineStyle: 'solid',
            lineColor: 'var(--gray)'
        }
    },
    series: [
        {
            values: [60, 55, 50, 50, 60, 50, 20, 15, 70, 50, 65],
            backgroundColor: 'var(--blue)',
            text: 'Jobs'
        },
        {
            values: [50, 45, 50, 45, 50, 30, 15, 5, 55, 50, 45],
            backgroundColor: 'var(--blue)',
            text: 'Candidates'
        },
        {
            values: [30, 30, 25, 30, 30, 35, 5, 0, 45, 30, 30],
            backgroundColor: 'var(--green)',
            text: 'Successful'
        }
    ]
};

zingchart.render({
    id: 'barChart',
    data: barChartData,
    height: '100%',
    width: '100%'
});

var hamburgerMenu = document.querySelector('.fa-bars');
var mobileMenu = document.querySelector('#mobile-menu');
var close = document.querySelector('#close');

hamburgerMenu.addEventListener('click', function () {
    if (hamburgerMenu) {
        mobileMenu.classList.add('open');
    }
});

close.addEventListener('click', function () {
    if (close) {
        mobileMenu.classList.remove('open');
    }
});