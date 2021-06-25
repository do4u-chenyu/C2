/**
 * Captures the full height document even if it's not showing on the screen or captures with the provided range of screen sizes.
 *
 * A basic example for taking a screen shot using TrifleJS which is sampled for https://nodejs-dersleri.github.io/
 *
 * usage : TrifleJS.exe screenshot.js {url} {output}  
 *
 * examples >
 *      TrifleJS.exe screenshot.js https://nodejs-dersleri.github.io/ C:\nodejs-dersleri.github.io.png
 *
 */

var args = require('system').args;

var fs = require('fs');


var page = new WebPage();

/**
 * if url address does not exist, exit phantom
 */
if ( 3 !== args.length ) {
    console.log('参数不对,需要Host和图片保存路径2个参数');
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
var ext = '.png';

/**
 * set if clipping ?
 * @type {boolean}
 */
var clipping = true;

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
    try {
         page.onError = function(msg, trace) {};
         page.open(refer, function (status) {
                              if ( 'success' !== status ) 
                              {
                                  console.log('Unable to load the url address!');
                              } else 
                              {
                                  var output, key;
                                  function render(n) {
                                      if ( !!n ) 
                                      {
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
         });
}
    catch(err){console.log(err+"异常")}
    
}

main()




