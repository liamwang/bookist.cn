import { message as Message } from 'ant-design-vue'
import { trimRight, queryString } from './string'

export class RequestError extends Error {
  handled: boolean
  constructor(message?: string, handled = true) {
    super(message)
    this.name = 'RequestError'
    this.handled = handled
  }
}

const errorMap: Record<string, string> = {
  '400': '请求出错',
  '401': '请重新登录',
  '403': '权限不足，禁止访问',
  '404': '资源不存在',
  '500': '服务器异常',
  default: '网络异常',
}

const handleError = (code: number, message?: string | undefined) => {
  if (code === 401) {
    location.href = import.meta.env.VITE_LOGIN_URL
  }
  return new RequestError(message || errorMap[code] || errorMap['default'])
}

class Http {
  private baseUrl: string
  private constructor(baseUrl: string) {
    this.baseUrl = trimRight(baseUrl, '/')
  }

  private static container: Record<string, Http> = {}

  static getInstance(baseUrl: string): Http {
    if (!this.container[baseUrl]) {
      this.container[baseUrl] = new Http(baseUrl)
    }
    return this.container[baseUrl]
  }

  request<D>(url: string, config: RequestConfig = {}): Promise<D | undefined> {
    let fullUrl = /^((https?:)|\/)/.test(url) ? url : `${this.baseUrl}/${url}`
    if (config.params && Object.keys(config.params).length > 0) {
      fullUrl += '?' + queryString(config.params)
    }

    return fetch(fullUrl, {
      method: 'get',
      headers: {
        'Content-Type': 'application/json',
      },
      body: config.data ? JSON.stringify(config.data) : undefined,
      ...config,
    })
      .then(async (response) => {
        if (response.ok) {
          const result: ApiResult<D> = await response.json()
          if (result.code === 0 || result.code === 200) {
            return result.data
          }
          if (result.code) {
            throw handleError(result.code, result.message)
          }
        }
        throw handleError(response.status)
      })
      .catch((e) => {
        Message.error(e instanceof RequestError ? e.message : '错误请求')
        throw e
      })
  }

  get<D>(url: string, params?: object) {
    return this.request<D>(url, { method: 'get', params })
  }

  post<D>(url: string, data: object) {
    return this.request<D>(url, { method: 'post', data })
  }

  put<D>(url: string, data: object) {
    return this.request<D>(url, { method: 'put', data })
  }

  patch<D>(url: string, data: object) {
    return this.request<D>(url, { method: 'patch', data })
  }

  delete<D>(url: string, params?: object) {
    return this.request<D>(url, { method: 'delete', params })
  }
}

const http = Http.getInstance(import.meta.env.VITE_BASE_API_URL)

export default http
