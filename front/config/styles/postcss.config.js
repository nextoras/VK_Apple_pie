const path = require('path');
const colors = path.resolve(__dirname, './colors.js');

module.exports = {
    plugins: [
        require('autoprefixer'),
        require('postcss-global-import')({
            sync: true,
        }),
        require('postcss-nested'),
        require('postcss-custom-properties')({
            importFrom: [colors],
            preserve: false,
        }),
        require('postcss-calc')({ mediaQueries: true }),
    ],
};
