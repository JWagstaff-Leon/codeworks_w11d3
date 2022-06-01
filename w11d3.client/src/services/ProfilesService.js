import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class ProfilesService
{
    async getAll()
    {
        const res = await api.get("api/profiles");
        logger.log("[ProfilesService > getAll > res.data]", res.data);
        AppState.profiles = res.data;
    }
}

export const profilesService = new ProfilesService();