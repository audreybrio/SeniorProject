const domain = "";
const apiPort = "8080";
let root = "http://" + domain + "/";
let apiRoot = "http://" + domain + ":" + apiPort + "/";
const URLS = {
    domain: domain,
    root: root,
    apiRoot: apiRoot,
    api: {
        scheduleBuilder: {
            getList: apiRoot + "schedule/getlist/",
            getSchedule: apiRoot + "schedule/getschedule/",
            createItem: apiRoot + "schedule/createItem/",
            updateItem: apiRoot + "schedule/updateItem/",
            deleteItem: apiRoot + "schedule/deleteItem/"
        },
    }
}

export default URLS