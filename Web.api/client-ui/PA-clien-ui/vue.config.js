// vue.config.js

import { IgnorePlugin } from 'webpack'

export const configureWebpack = {
  plugins: [new IgnorePlugin(/^\.\/locale$/, /moment$/)],
}
