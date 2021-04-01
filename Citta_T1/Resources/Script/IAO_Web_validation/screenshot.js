/**
 * Captures the full height document even if it's not showing on the screen or captures with the provided range of screen sizes.
 *
 * A basic example for taking a screen shot using phantomjs which is sampled for https://nodejs-dersleri.github.io/
 *
 * usage : phantomjs responsive-screenshot.js {refer}[output file]  
 *
 * examples >
 *      phantomjs responsive-screenshot.js https://nodejs-dersleri.github.io/
 *      phantomjs responsive-screenshot.js https://nodejs-dersleri.github.io/ pdf
 *      phantomjs responsive-screenshot.js https://nodejs-dersleri.github.io/ true
 *      phantomjs responsive-screenshot.js https://nodejs-dersleri.github.io/ png true
 *
 * @author Salih sagdilek <salihsagdilek@gmail.com>
 */

/**
 * http://phantomjs.org/api/system/property/args.html
 *
 * Queries and returns a list of the command-line arguments.
 * The first one is always the script name, which is then followed by the subsequent arguments.
 */
var args = require('system').args;
/**
 * http://phantomjs.org/api/fs/
 *
 * file system api
 */
var fs = require('fs');

/**
 * http://phantomjs.org/api/webpage/
 *
 * Web page api
 */
var page = new WebPage();

/**
 * if url address does not exist, exit phantom
 */
if ( 1 === args.length ) {
    console.log('Url address is required');
    phantom.exit();
}

/**
 *  setup url address (second argument);
 */

var refer = args[1];
var outputPath = args[2];


/**
 * set output extension format
 * @type {*}
 */
var ext = getFileExtension();

/**
 * set if clipping ?
 * @type {boolean}
 */
var clipping = getClipping();

/**
 * setup viewports
 */
var viewports = [
    {
        width : 1366,
        height : 768
    }
];
function main()
{
    try{page.open(refer, function (status) {
    if ( 'success' !== status ) {
        console.log('Unable to load the url address!');
    } else {
        var output, key;
        function render(n) {
            if ( !!n ) {
                key = n - 1;
                page.viewportSize = viewports[key];
                console.log('Saving ' + outputPath);
                page.render(outputPath);
                render(key);
            }
        }

        render(viewports.length);
    }
    phantom.exit();
});}
    catch(err){console.log(err+"异常")}
    
}
main()

/**
 * filename generator helper
 * @param viewport
 * @returns {string}
 */
function getFileName(urlAddress) {
    return urlAddress.replace(":","冒号")
}

/**
 * output extension format helper
 *
 * @returns {*}
 */
function getFileExtension() {
    if ( 'true' != args[2] && !!args[2] ) {
        return '.' + args[2];
    }
    return '.png';
}

/**
 * check if clipping
 *
 * @returns {boolean}
 */
function getClipping() {
    if ( 'true' == args[3] ) {
        return !!args[3];
    } else if ( 'true' == args[2] ) {
        return !!args[2];
    }
    return false;
}
/**
* get referrer
**/

function getRefer(url){
    if( url.search("http://")!=-1 || url.search("https://")!=-1)
    {
       return url 
    }
    return "http://" + url
}
