var mycontainer1_1Chart = echarts.init(document.getElementById('container1_1'), "phx");
var container1_1option = {

    legend: {},
    xAxis: {
        type: 'category'
    },
    yAxis: {},
    tooltip: {},
    dataset: {
        source: [['产品', '2015', '2016', '2017'], ['中国', '43.3', '85.8', '93.7'], ['美国', '83.1', '73.4', '55.1'], ['日本', '86.4', '65.2', '82.5'], ['英国', '72.4', '53.9', '39.1']]
    },
    series: [{
        type: 'bar'
    },
    {
        type: 'bar'
    },
    {
        type: 'bar'
    }]
};
mycontainer1_1Chart.setOption(container1_1option);

var mycontainer1_2Chart = echarts.init(document.getElementById('container1_2'), "phx");
var container1_2option = {

    legend: {},
    xAxis: {
        type: 'category'
    },
    yAxis: {
        type: 'value'
    },
    tooltip: {},
    dataset: {
        source: [['产品', '2015', '2016', '2017'], ['中国', '43.3', '85.8', '93.7'], ['美国', '83.1', '73.4', '55.1'], ['日本', '86.4', '65.2', '82.5'], ['英国', '72.4', '53.9', '39.1']]
    },
    series: [{
        type: 'line'
    },
    {
        type: 'line'
    },
    {
        type: 'line'
    }]
};
mycontainer1_2Chart.setOption(container1_2option);

var mycontainer1_3Chart = echarts.init(document.getElementById('container1_3'), "phx");
var container1_3option = {

    legend: {},
    xAxis: {},
    yAxis: {},
    tooltip: {},
    dataset: {
        source: [['产品', '2015', '2016', '2017'], ['中国', '43.3', '85.8', '93.7'], ['美国', '83.1', '73.4', '55.1'], ['日本', '86.4', '65.2', '82.5'], ['英国', '72.4', '53.9', '39.1']]
    },
    series: [{
        type: 'scatter'
    },
    {
        type: 'scatter'
    },
    {
        type: 'scatter'
    }]
};
mycontainer1_3Chart.setOption(container1_3option);

var mycontainer3_1Chart = echarts.init(document.getElementById('container3_1'), "phx");
var container3_1option = {

    legend: {},
    xAxis: {
        type: 'category'
    },
    yAxis: {
        type: 'value'
    },
    tooltip: {},
    dataset: {
        source: [['产品', '2015', '2016', '2017'], ['中国', '43.3', '85.8', '93.7'], ['美国', '83.1', '73.4', '55.1'], ['日本', '86.4', '65.2', '82.5'], ['英国', '72.4', '53.9', '39.1']]
    },
    series: [{
        type: 'line',
        smooth: 'true'
    },
    {
        type: 'line',
        smooth: 'true'
    },
    {
        type: 'line',
        smooth: 'true'
    }]
};
mycontainer3_1Chart.setOption(container3_1option);

var mycontainer3_2Chart = echarts.init(document.getElementById('container3_2'), "phx");
var container3_2option = {

    legend: {},
    xAxis: {
        type: 'category'
    },
    yAxis: {},
    tooltip: {},
    dataset: {
        source: [['产品', '2015', '2016', '2017'], ['中国', '43.3', '85.8', '93.7'], ['美国', '83.1', '73.4', '55.1'], ['日本', '86.4', '65.2', '82.5'], ['英国', '72.4', '53.9', '39.1']]
    },
    series: [{
        type: 'bar',
        stack: '汇总'
    },
    {
        type: 'bar',
        stack: '汇总'
    },
    {
        type: 'bar',
        stack: '汇总'
    }]
};
mycontainer3_2Chart.setOption(container3_2option);


var mycontainer3_3Chart = echarts.init(document.getElementById('container3_3'), "phx");
var container3_3option = {
    legend: {
        left: 'center',
        orient: 'horizontal'
    },
    tooltip: {},
    dataset: {
        source: [['产品', '2015', '2016', '2017'], ['中国', '43.3', '85.8', '93.7'], ['美国', '83.1', '73.4', '55.1'], ['日本', '86.4', '65.2', '82.5'], ['英国', '72.4', '53.9', '39.1']]
    },
    series: [{
        type: 'pie',
        emphasis: {
            itemStyle: {
                shadowBlur: 10,
                shadowColor: 'rgba(0, 0, 0, 0.5)',
                shadowOffsetX: 0
            }
        }
    }]
};
mycontainer3_3Chart.setOption(container3_3option);


var mycontainer2_1Chart = echarts.init(document.getElementById('container2_1'), "phx");
function randomData() {  
     return Math.round(Math.random()*500);  
} 
var mydata = [ 
{name: '北京',value: '100' },
{name: '天津',value: randomData() },
{name: '广东',value: randomData() },
{name: '湖北',value: randomData() },
{name: '湖南',value: randomData() },
{name: '江苏',value: randomData() },
{name: '河北',value: randomData() },
{name: '河南',value: randomData() }
]
var container2_1option = {  
                title: {  
                    text: '全国地图大数据',  
                    subtext: '',  
                    x:'center'  
                },  
                tooltip : {  
                    trigger: 'item'  
                },  
                
                //左侧小导航图标
                visualMap: {  
                    show : true,  
                    x: 'left',  
                    y: 'center',  
                    splitList: [   
                        {start: 500, end:600},{start: 400, end: 500},  
                        {start: 300, end: 400},{start: 200, end: 300},  
                        {start: 100, end: 200},{start: 0, end: 100},  
                    ] 

                },  
                
                //配置属性
                series: [{  
                    name: '数据',  
                    type: 'map',  
                    mapType: 'china',   
                    roam: true,  
                    label: {  
                        normal: {  
                            show: true  //省份名称  
                        },  
                        emphasis: {  
                            show: false  
                        }  
                    },  
                    data:mydata  //数据
                }]  
            };  

mycontainer2_1Chart.setOption(container2_1option);
