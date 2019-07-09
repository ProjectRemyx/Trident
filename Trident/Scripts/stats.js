//Referenced from: https://codepen.io/derekmorash/pen/QGWjJx
//////////////////////////////////////////////
// Data //////////////////////////////////////
//////////////////////////////////////////////
// Sample Data
var data = [
    { hit: 1, damage: 180 },
    { hit: 2, damage: 500 },
    { hit: 3, damage: 250 }
];

//////////////////////////////////////////////
// Chart Config /////////////////////////////
//////////////////////////////////////////////

// Set the dimensions of the canvas / graph
var margin = { top: 30, right: 20, bottom: 30, left: 50 },
    width, // width gets defined below
    height = 250 - margin.top - margin.bottom;

// Set the scales ranges
var xScale = d3.scaleTime();
var yScale = d3.scaleLinear().range([height, 0]);

// Define the axes
var xAxis = d3.axisBottom().scale(xScale);
var yAxis = d3.axisLeft().scale(yScale);

// create a line
var line = d3.line();

// Add the svg canvas
var svg = d3.select("#bar-chart")
    .append("svg")
    .attr("height", height + margin.top + margin.bottom);

var artboard = svg.append("g")
    .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

// set the domain range from the data
xScale.domain(d3.extent(data, function (d) { return d.hit; }));
yScale.domain([
    d3.min(data, function (d) { return Math.floor(d.damage - 200); }),
    d3.max(data, function (d) { return Math.floor(d.damage + 200); })
]);

// draw the line created above
var path = artboard.append("path").data([data])
    .style('fill', 'none')
    .style('stroke', 'steelblue')
    .style('stroke-width', '2px');

// Add the X Axis
var xAxisEl = artboard.append("g")
    .attr("transform", "translate(0," + height + ")");

// Add the Y Axis
// we aren't resizing height in this demo so the yAxis stays static, we don't need to call this every resize
var yAxisEl = artboard.append("g")
    .call(yAxis);

//////////////////////////////////////////////
// Drawing ///////////////////////////////////
//////////////////////////////////////////////
function drawChart() {
    // reset the width
    width = parseInt(d3.select('body').style('width'), 10) - margin.left - margin.right;

    // set the svg dimensions
    svg.attr("width", width + margin.left + margin.right);

    // Set new range for xScale
    xScale.range([0, width]);

    // give the x axis the resized scale
    xAxis.scale(xScale);

    // draw the new xAxis
    xAxisEl.call(xAxis);

    // specify new properties for the line
    line.x(function (d) { return xScale(d.hit); })
        .y(function (d) { return yScale(d.damage); });

    // draw the path based on the line created above
    path.attr('d', line);
}

// call this once to draw the chart initially
drawChart();

//////////////////////////////////////////////
// Resizing //////////////////////////////////
//////////////////////////////////////////////

// redraw chart on resize
window.addEventListener('resize', drawChart);