const path = require('path');

module.exports = {
  mode: 'development',
  entry: './src/index.js',
  output: {
    path: path.resolve('..', 'the key - technology GmbH', 'wwwroot', 'js', 'react', 'wordpress-reader'),
    filename: 'bundle-websocket.js',
    library: 'wp_wc',
  },
  module: {
    rules: [
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        use: ['babel-loader']
      },
    ]
  },
  // watch: true,
  devtool: 'inline-source-map',
};