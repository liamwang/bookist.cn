import { reactive, ref } from 'vue'

const useAsync = () => {
  const loading = ref(false)
  const data = reactive<any>({})
  const error = ref()

  const run = async (promise: Promise<any>) => {
    if (!promise || !promise.then) {
      throw new Error('参数必须是 Promise 类型')
    }
    loading.value = true
    try {
      const result = await promise
      Object.assign(data, result)
      return result
    } catch (e) {
      return new Promise(() => null)
    } finally {
      loading.value = false
    }
  }

  return { run, loading, data, error }
}

export default useAsync
