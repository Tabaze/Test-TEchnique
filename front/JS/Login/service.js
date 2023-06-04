export function login(dt) {
    let lt = ajaxJSONPost('api/User/signIn', dt);
    return lt
}
export function getAll() {
    let lt = ajaxJSONGet('api/User/getAll', '')
    return lt
}