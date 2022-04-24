const fs = require('fs')
const path = require('path')

const baseFolder =
    process.env.APPDATA !== undefined && process.env.APPDATA !== ''
        ? `${process.env.APPDATA}/ASP.NET/https`
        : `${process.env.HOME}/.aspnet/https`;

const certificateArg = process.argv.map(arg => arg.match(/--name=(?<value>.+)/i)).filter(Boolean)[0];
const certificateName = certificateArg ? certificateArg.groups.value : "VueFrontend";

if (!certificateName) {
    console.error('Invalid certificate name. Run this script in the context of an npm/yarn script or pass --name=<<app>> explicitly.')
    process.exit(-1);
}

const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

module.exports = {
    publicPath: process.env.NODE_ENV === 'production' // assuming both should be '/'. default is '/'
        ? '/'  // production
        : '/', // development 
    assetsDir: process.env.NODE_ENV === 'production' // prod & dev can differ. default is ''
        ? ''  // production
        : '', // development 
    indexPath: process.env.NODE_ENV === 'production' // probably can be different. default is 'index.html'
        ? 'index.html' // production
        : 'index.html', // development
    devServer: { // development only
        https: {
            key: fs.readFileSync(keyFilePath),
            cert: fs.readFileSync(certFilePath),
        },

        // Tentative fix to allow AJAX-type requests through to the server
        // Without this, those requests get blocked due to this CORS error:
        // https://developer/mozilla.org/en-US/docs/Web/HTTP/CORS/Errors/CORSMissingAllowOrigin
        proxy: {
            '^/api': {
                target: 'https://localhost:5001/'
            }
        },
        port: 5002
    }
}