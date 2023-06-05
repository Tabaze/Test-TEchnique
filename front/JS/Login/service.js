export function login(dt) {
    let lt = ajaxJSON('api/User/signIn', dt);
    return lt
}
export function getAll() {
    let lt = ajaxJSON('api/User/getAll', '')
    return lt
}