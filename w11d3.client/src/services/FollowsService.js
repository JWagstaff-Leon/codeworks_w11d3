import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class FollowsService
{
    async getFollowers(id)
    {
        const res = await api.get("api/profiles/" + id + "/followers");
        logger.log("[FollowsService > getFollowers > res.data]", res.data);
        return res.data;
    }

    async getFollowings(id)
    {
        const res = await api.get("api/profiles/" + id + "/following");
        logger.log("[FollowsService > getFollowing > res.data]", res.data);
        return res.data;
    }

    async follow(id)
    {
        const res = await api.post("api/follow/" + id);
        logger.log("[FollowsService > follow > res.data]", res.data);
        AppState.activeProfileFollowers.push(res.data);
    }

    async unfollow(id)
    {
        id = AppState.userFollowings.find(follow => follow.id === AppState.activeProfile.id).followId;
        const res = await api.delete("api/follow/" + id);
        logger.log("[FollowsService > unfollow > res.data]", res.data);
        AppState.activeProfileFollowers.push(res.data);
    }
}

export const followsService = new FollowsService();