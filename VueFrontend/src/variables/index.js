const domain = "localhost";
const apiPort = "5003";
//const domain = "ec2-13-52-181-69.us-west-1.compute.amazonaws.com";
//const apiPort = "8080";
let root = `http://${domain}/`;
let apiRoot = `http://${domain}:${apiPort}/api/`;
const URLS = {
    domain: domain,
    root: root,
    apiRoot: apiRoot,
    api: {
        scheduleBuilder: {
            getList: apiRoot + "schedule/getlist",
            getSchedule: apiRoot + "schedule/getschedule",
            newSchedule: apiRoot + "schedule/newschedule",
            saveSchedule: apiRoot + "schedule/saveschedule",
            createItem: apiRoot + "schedule/createItem",
            updateItem: apiRoot + "schedule/updateItem",
            deleteItem: apiRoot + "schedule/deleteItem"
        },
        scheduleComparison: {
            getComparison: apiRoot + "schedulecomparison/getcomparison",
        }
    }
}

export default URLS